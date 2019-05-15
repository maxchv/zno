using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZnoModelLibrary.Entities;
using ZnoModelLibrary.Interfaces;

namespace ZnoApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с тестом и его настройками
    /// </summary>
    [Authorize(Roles = "Admin")]
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class TestController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Получение списка всех тестов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение списка всех предметов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение списка всех уровней сложности
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLevelOfDifficulty()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создание настроек для теста по определенному предмету
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTestSettings(TestSettings settings)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обновление настроек для теста по определенному предмету
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTestSettings(TestSettings settings)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удаление натстроек для теста по определенному предмету
        /// </summary>
        /// <param name="settingsId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTestSettings(int settingsId)
        {
            throw new NotImplementedException();
        }
    }
}