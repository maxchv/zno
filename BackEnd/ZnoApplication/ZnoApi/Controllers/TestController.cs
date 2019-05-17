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
        /// <returns>Список всех тестов</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            return Ok(await _unitOfWork.Tests.FindAll());
        }

        /// <summary>
        /// Получение списка всех предметов
        /// </summary>
        /// <returns>Список всех предметов</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            return Ok(await _unitOfWork.Subjects.FindAll());
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
        /// <param name="settings">Настройки теста</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTestSettings(TestSettings settings)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.TestSettings.Insert(settings);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление настроек для теста по определенному предмету
        /// </summary>
        /// <param name="settings">Настройки теста</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTestSettings(TestSettings settings)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.TestSettings.Insert(settings);
                _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаление настроек для теста по определенному предмету
        /// </summary>
        /// <param name="settingsId">Идентификатор настроек теста</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTestSettings(int settingsId)
        {
            var settings = _unitOfWork.TestSettings.FindById(settingsId);
            if (settings != null)
            {
                _unitOfWork.BeginTransaction();
                try
                {
                    await _unitOfWork.TestSettings.Delete(settings.Id);
                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                    return Ok();
                }catch(Exception ex)
                {
                    _unitOfWork.Rollback();
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("TestSettings is not found.");
            }
        }
    }
}