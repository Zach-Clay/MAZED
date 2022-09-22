using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using api_backend.Data;
using api_backend.Models;
using Microsoft.AspNetCore.Mvc;
using api_backend.Logic;

namespace api_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        //Get the user based off username
        [HttpGet]
        [Route("username")]
        public IActionResult GetUser(string username)
        {
            try
            {
                return Ok(UserLogic.getUser(username));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Add user to the DB
        [HttpPost]
        public IActionResult AddUser([FromBody]User user)
        {
            try
            {
                return Ok(UserLogic.addUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
