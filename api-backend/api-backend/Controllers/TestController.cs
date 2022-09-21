using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_backend.Models;
using api_backend.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_backend.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class ValuesController : Controller
    {
        // GET: api/test
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(TestLogic.getValues());
        }

        // GET api/test/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("Not Implemented");
        }

        // POST api/test
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok("Not Implemented");
        }

        // PUT api/test/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok("Not Implemented");
        }

        // DELETE api/test/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("Not Implemented");
        }
    }
}

