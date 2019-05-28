using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Zno.DAL.Entities;
using Zno.DAL.Interfaces;


namespace Zno.Server.Controllers
{
    /// <summary>
    /// Контроллер тестирования
    /// </summary>
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class TestingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;

        public TestingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Генерирование нового теста по указанному предмету
        /// </summary>
        /// <param name="subjectId">Индентификатор предмета по которому пользователь хочет пройти тест</param>
        /// <returns>Список сгенерированных вопросов</returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateNewTest(int subjectId)
        {
            var generatedTestQuestions = new List<Question>();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            // Создаем тест и задаем в него параметры 
            GeneratedTest generatedTest = new GeneratedTest();
            generatedTest.User = currentUser;
            generatedTest.StartTime = DateTime.Now;
            generatedTest.EndTime = DateTime.Now.AddMinutes(2);

            // Формируем список вопросов

            // Находим настройки теста по предмету
            var testSettings = await _unitOfWork.TestSettings.FindAll();
            var testSetting = testSettings.ToList().Where(s => s.Subject.Id == subjectId).SingleOrDefault();

            // Если настроки теста найдены
            if (testSetting != null)
            {
                // Находим тесты
                var tests = testSetting.Tests;
                var questionsAll = await _unitOfWork.Questions.FindAll();
                // Формируем список вопросов которые относятся к тем тестам, заданным в TestSetting
                var questionsByTest = new List<Question>();
                for (int i = 0; i < questionsAll.ToList().Count(); i++)
                {
                    if (tests.Contains(questionsAll.ToList()[i].Test))
                    {
                        questionsByTest.Add(questionsAll.ToList()[i]);
                    }
                }
                var questions = new List<List<Question>>();
                // Перебираем каждую категорию сложности и формируем список вопросов с каждой категорией сложности
                Random r = new Random();
                for (int i = 0; i < testSetting.AnswerTypes.Count(); i++)
                {
                    var questionsOfOneType = questionsAll.ToList().Where(q => q.AnswerType == testSetting.AnswerTypes.ToList()[i]);

                    for (int q = 0; q < testSetting.NumberOfQuestions; q++)
                    {
                        int randomQuestionIdx = r.Next(testSetting.Tests.Count());
                        var randomQuestion = questionsAll.ToList()[randomQuestionIdx];
                        generatedTestQuestions.Add(randomQuestion);
                    }
                }
                generatedTest.Questions = generatedTestQuestions;

                // Сохранение GeneratedTest
                _unitOfWork.BeginTransaction();
                try
                {
                    await _unitOfWork.GeneratedTests.Insert(generatedTest);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                    return Ok(generatedTest.Questions);
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Test settings not found.");
            }
        }

        /// <summary>
        /// Принятие ответа на указанный вопрос указанного теста
        /// </summary>
        /// <param name="questionId">Индентификатор вопроса на который текущий пользователь отвечает</param>
        /// <param name="answer">Ответ на вопрос</param>
        /// <param name="generatedTestId">Идентификатор сгенерированного теста</param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> SavingAnswer(int questionId, object answer, int generatedTestId)
        {
            var generatedTest = await _unitOfWork.GeneratedTests.FindById(generatedTestId);
            var question = await _unitOfWork.Questions.FindById(questionId);

            // Если текущее время не вышло за пределы доступные на тест
            if (DateTime.Now < generatedTest.EndTime)
            {
                // Добавляем ответ пользователя в тест
                var userAnswer = new UserAnswer();
                userAnswer.Question = question;
                userAnswer.Answer = Newtonsoft.Json.JsonConvert.SerializeObject(answer);
                generatedTest.Answers.Add(userAnswer);

                // Увеличиваем текущую позицию в тесте (вопроса)
                generatedTest.CurrentPosition++;

                // Сохраняем изменения в GeneratedTest
                _unitOfWork.BeginTransaction();
                try
                {
                    await _unitOfWork.GeneratedTests.Update(generatedTest);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                    return Ok();
                }
                catch(Exception ex)
                {
                    _unitOfWork.Rollback();
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Time to test out.");
            }

            return BadRequest();
        }

        /// <summary>
        /// Завершение теста и подсчет результата (без учёта ответов типа Task)
        /// </summary>
        /// <param name="generatedTestId">Идентификатор сгенерированного теста</param>
        /// <returns>Сумма баллов за тест</returns>
        [Authorize(Roles = "User;Admin")]
        [HttpGet]
        public async Task<IActionResult> CompletingTestAndGetResult(int generatedTestId)
        {
            var generatedTest = await _unitOfWork.GeneratedTests.FindById(generatedTestId);

            // Меняем статус на false (закончили тест)
            generatedTest.Status = false;

            // Увеличиваем сумму баллов, если ответ пользователя и правильный совпадают
            for(int i = 0; i < generatedTest.Answers.ToList().Count(); i++)
            {
                if (generatedTest.Answers.ToList()[i].Question.AnswerType != AnswerType.Task)
                {
                    if(generatedTest.Answers.ToList()[i].Answer== generatedTest.Answers.ToList()[i].Question.AnswerJson)
                    {
                        generatedTest.Score++;
                    }
                }
            }

            // Сохраняем изменения в GeneratedTest
            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GeneratedTests.Update(generatedTest);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(generatedTest.Score);
            }
            catch(Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Возвращает оставщееся время по указанному тесту
        /// </summary>
        /// <param name="generatedTestId">Идентификатор сгенерированного теста</param>
        /// <returns>Оставшееся время на выполнение теста</returns>
        [Authorize(Roles = "User;Admin")]
        [HttpGet]
        public async Task<IActionResult> GetRemainingTime(int generatedTestId)
        {
            var generatedTest = await _unitOfWork.GeneratedTests.FindById(generatedTestId);
            if (DateTime.Now > generatedTest.EndTime)
            {
                return BadRequest("Time to test out.");
            }
            else {
                return Ok(generatedTest.EndTime - DateTime.Now);
            }
        }
    }
}