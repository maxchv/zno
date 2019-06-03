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
    //[Authorize(Roles = "Admin")]
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
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateNewTest(int subjectId)
        {
            // Необходимо сгенерировать новый тест для текущего пользователя
            // по указанному предмету учитывая настройки для теста
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var generatedTestQuestions = new List<Question>();
            //var currentUser = await _unitOfWork.Users.FindByLogin("admin@domain.com");

            // Создаем тест и задаем в него параметры 
            GeneratedTest generatedTest = new GeneratedTest();
            generatedTest.User = currentUser;
            generatedTest.Answers = new List<UserAnswer>();
            generatedTest.StartTime = DateTime.Now;

            // Формируем список вопросов

            // Находим настройки теста по предмету
            var testSettings = await _unitOfWork.TestSettings.FindAll();
            var testSetting = testSettings.ToList().Where(s => s.Subject.Id == subjectId).SingleOrDefault();

            // Если настроки теста найдены
            if (testSetting != null)
            {
                // Если вопросов для теста в настройках указано больше 0
                if (testSetting.NumberOfQuestions > 0)
                {
                    generatedTest.EndTime = DateTime.Now.AddMinutes(testSetting.TestingTime);

                    // Находим тесты
                    var tests = testSetting.Tests;
                    if (testSetting.Tests.Count > 0)
                    {
                        var questionsAll = await _unitOfWork.Questions.FindAll();
                        if (questionsAll.Count() > 0)
                        {
                            // Если всех вопросов больше, либо равно кол-во вопросов в настройках к тесту
                            if (questionsAll.Count() >= testSetting.Tests.Count())
                            {
                                // Формируем список вопросов которые относятся к тем тестам, заданным в TestSetting
                                var questionsByTest = new List<Question>();
                                for (int i = 0; i < questionsAll.ToList().Count(); i++)
                                {
                                    if (tests.Contains(questionsAll.ToList()[i].Test))
                                    {
                                        questionsByTest.Add(questionsAll.ToList()[i]);
                                    }
                                }
                                // Если доступных вопросов больше, либо равно кол-во вопросов в настройках к тесту
                                if (questionsByTest.Count >= testSetting.NumberOfQuestions)
                                {
                                    var questions = new List<List<Question>>();
                                    // Перебираем каждую категорию сложности и формируем список вопросов с каждой категорией сложности
                                    Random r = new Random();
                                    int qNum = 0;
                                    do
                                    {
                                        for (int i = 0; i < testSetting.QuestionTypes.Count(); i++)
                                        {
                                            // Вопросы по одной категории
                                            var questionsOfOneType = questionsByTest.ToList()
                                                .Where(q => q.QuestionType == testSetting.QuestionTypes.ToList()[i].QuestionType).ToList();

                                            // Выбираем один любой вопрос из этой категории. Которого ещё нет в списке
                                            int randomQuestionIdx = r.Next(questionsOfOneType.ToList().Count);
                                            var randomQuestion = questionsOfOneType.ToList()[randomQuestionIdx];
                                            if (!generatedTestQuestions.Contains(randomQuestion))
                                            {
                                                generatedTestQuestions.Add(randomQuestion);
                                                qNum++;
                                            }
                                        }
                                    } while (qNum < testSetting.NumberOfQuestions);

                                    generatedTest.Questions = generatedTestQuestions;

                                    // Сохранение GeneratedTest
                                    _unitOfWork.BeginTransaction();
                                    try
                                    {
                                        await _unitOfWork.GeneratedTests.Insert(generatedTest);
                                        await _unitOfWork.SaveChanges();
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
                                    return BadRequest("Not enough test questions.");
                                }
                            }
                            else
                            {
                                return BadRequest("Not enough test questions.");
                            }
                        }
                        else
                        {
                            return BadRequest("Are questions is not found.");
                        }
                    }
                    else
                    {
                        return BadRequest("Are tests is not found.");
                    }
                }
                else
                {
                    return BadRequest("Error set number of questions in test settings.");
                }
            }
            else
            {
                return BadRequest("Test settings is not found.");
            }
        }

        /// <summary>
        /// Принятие ответа на указанный вопрос указанного теста
        /// </summary>
        /// <param name="questionId">Индентификатор вопроса на который текущий пользователь отвечает</param>
        /// <param name="answers">Ответы на вопрос</param>
        /// <param name="generatedTestId">Идентификатор сгенерированного теста</param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> SavingAnswer(int questionId, [FromBody]List<Answer> answers, int generatedTestId)
        {
            var generatedTest = await _unitOfWork.GeneratedTests.FindById(generatedTestId);
            var question = await _unitOfWork.Questions.FindById(questionId);

            // Если текущее время не вышло за пределы доступные на тест
            if (DateTime.Now < generatedTest.EndTime)
            {
                if (generatedTest.Questions.Contains(question))
                {
                    // Добавляем ответы пользователя в тест
                    var userAnswer = new UserAnswer();
                    userAnswer.Question = question;

                    userAnswer.Answers = answers;
                    try
                    {
                        generatedTest.Answers.Add(userAnswer);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }

                    // Увеличиваем текущую позицию в тесте (вопроса)
                    generatedTest.CurrentPosition++;

                    // Сохраняем изменения в GeneratedTest
                    _unitOfWork.BeginTransaction();
                    try
                    {
                        await _unitOfWork.GeneratedTests.Update(generatedTest);
                        await _unitOfWork.SaveChanges();
                        _unitOfWork.Commit();
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        _unitOfWork.Rollback();
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("A question not found in the test.");
                }
            }
            else
            {
                return BadRequest("Time to test out.");
            }
        }

        /// <summary>
        /// Возвращает текущий вопрос
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentQuestion()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Завершение теста и подсчет результата (без учёта ответов типа Task)
        /// (в процессе реализации, ошибка с получением ответов в GeneratedTest)
        /// </summary>
        /// <param name="generatedTestId">Идентификатор сгенерированного теста</param>
        /// <returns>Сумма баллов за тест</returns>
        [Authorize(Roles = "User;Admin")]
        [HttpGet]
        public async Task<IActionResult> CompletingTestAndGetResult(int generatedTestId)
        {
            var generatedTest = await _unitOfWork.GeneratedTests.FindById(generatedTestId);
            //var userAnswers = await _unitOfWork.UserAnswers.FindAll();

            // Меняем статус на false (закончили тест)
            generatedTest.Status = false;

            // Увеличиваем сумму баллов, если ответ пользователя и правильный совпадают
            for (int i = 0; i < generatedTest.Answers.ToList().Count(); i++)
            {
                if (generatedTest.Answers.ToList()[i].Question.QuestionType.Name != "Task")
                {
                    // Ответы пользователя - generatedTest.Answers.ToList()[i].Answers
                    // Ответы правильные - generatedTest.Answers.ToList()[i].Question.Answers

                    List<String> validAnswers = new List<String>(); // Правильные ответы на вопрос
                    List<String> userAnswers = new List<String>(); // Ответы пользователя
                    List<Boolean> fieldUserAnswers = new List<Boolean>(); // Список-поле ответов пользователя, в котором 
                    // true - правильный ответ, и false - не правильный

                    // Добавляем в список ответы пользователя
                    userAnswers = generatedTest.Answers.ToList()[i].Answers
                        .Select(a => a.Content).ToList();

                    // Добавляем в список правильные ответы на вопрос
                    validAnswers = generatedTest.Answers.ToList()[i].Question.Answers
                        .Where(a => a.RightAnswer == true).Select(s => s.Content).ToList();

                    // Перебираем правильные ответы, и ищем их у пользователя. Формируем список ответов пользователя
                    // в виде bool списка. Где true - правильный ответ, и false - не правильный
                    for (int j = 0; j < validAnswers.Count(); j++)
                    {
                        if (!userAnswers.Contains(validAnswers[j]))
                        {
                            fieldUserAnswers.Add(true);
                        }
                        else
                        {
                            fieldUserAnswers.Add(false);
                        }
                    }

                    // Кол-во правильных ответов у пользователя
                    int countValidAnswers = fieldUserAnswers.Where(a => a == true).ToList().Count();

                    // Если кол-во правильно отвеченых вариантов столько, сколько всего отвеченых вариантов,
                    // а также если кол-во отвеченых вариантов равно кол-ву правильных ответов
                    if (countValidAnswers == fieldUserAnswers.Count() &&
                        fieldUserAnswers.Count() == validAnswers.Count())
                    {
                        generatedTest.Score++; // Увеличиваем балл за тест
                    }
                }
            }

            // Сохраняем изменения в GeneratedTest
            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.GeneratedTests.Update(generatedTest);
                await _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return Ok(generatedTest.Score);
            }
            catch (Exception ex)
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
            else
            {
                return Ok(generatedTest.EndTime - DateTime.Now);
            }
        }
    }
}