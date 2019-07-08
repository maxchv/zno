using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Zno.DAL.Interfaces;

namespace Zno.Server.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var num1obj = _unitOfWork.Answers.FindById(1).Result;
            var num1 = JsonConvert.SerializeObject(num1obj);
            var num2 = JsonConvert.SerializeObject(_unitOfWork.Answers.FindById(2).Result);
            var num3 = JsonConvert.SerializeObject(_unitOfWork.Answers.FindById(3).Result);
            var num4 = JsonConvert.SerializeObject(_unitOfWork.Answers.FindById(4).Result);

            return new string[] { num1, num2, num3, num4 };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
