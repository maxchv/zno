using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Zno.DAL.Context;
using Zno.DAL.Implementation;
using Zno.DAL.Interfaces;
using Zno.Server.Controllers;

namespace Zno.Tests
{
    public class TestControllerTest
    {
        private const string CONNECTION_STRING = "server=104.248.135.234;port=3306;userid=znouser;password=znopass;database=zno;";
        private TestController _controller;

        public TestControllerTest()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var options = optionsBuilder.UseMySql(CONNECTION_STRING).Options;

            var context = new ApplicationDbContext(options);

            IUnitOfWork unitOfWork = new MySqlUnitOfWork(context);

            _controller = new TestController(unitOfWork);
        }

        [Fact]
        public void GetAllTests_Test()
        {
            var result = _controller.GetAllTests();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllSubjects_Test()
        {
            var result = _controller.GetAllSubjects();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllLevelOfDifficulty_Test()
        {
            var result = _controller.GetAllLevelOfDifficulty();

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}