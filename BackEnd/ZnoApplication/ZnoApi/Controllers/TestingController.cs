using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateNewTest(int subjectId)
        {
            // Необходимо сгенерировать новый тест для текущего пользователя
            // по указанному предмету учитывая настройки для теста
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Принятие ответа на указанный вопрос указанного теста
        /// </summary>
        /// <param name="questionId">Индентификатор вопроса на который текущий пользователь отвечает</param>
        /// <param name="answer">Ответ на вопрос</param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> SavingAnswer(int questionId, object answer)
        {
            throw new NotImplementedException();
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
        /// Завершение теста и подсчет результата
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [Authorize(Roles = "User;Admin")]
        [HttpGet]
        public async Task<IActionResult> CompletingTestAndGetResult(int testId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает оставщееся время по указанному тесту
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [Authorize(Roles = "User;Admin")]
        [HttpGet]
        public async Task<IActionResult> GetRemainingTime(int testId)
        {
            throw new NotImplementedException();
        }
    }
}