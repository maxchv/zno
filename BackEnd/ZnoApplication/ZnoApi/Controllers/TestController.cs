using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;

namespace Zno.Server.Controllers
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
            try
            {
                var test = await _unitOfWork.Tests.FindAll();
                return Ok(test);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех предметов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var allSubjects = await _unitOfWork.Subjects.FindAll();
                return Ok(allSubjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение списка всех уровней сложности
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLevelOfDifficulty()
        {
            try
            {
                var types = await _unitOfWork.QuestionTypes.FindAll();
                return Ok(types);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создание настроек для теста по определенному предмету
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTestSettings([FromBody]TestSettings settings)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                await _unitOfWork.TestSettings.Insert(settings);
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

        /// <summary>
        /// Обновление настроек для теста по определенному предмету
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTestSettings([FromBody]TestSettings settings)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                await _unitOfWork.TestSettings.Update(settings);
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

        /// <summary>
        /// Удаление настроек для теста по определенному предмету
        /// </summary>
        /// <param name="settingsId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTestSettings(int settingsId)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                await _unitOfWork.TestSettings.Delete(settingsId);
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
    }
}