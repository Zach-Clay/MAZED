using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using api_backend.Data;
using api_backend.Models;
using Microsoft.AspNetCore.Mvc;
using api_backend.Logic;
using MazedDB.Models;

namespace api_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        //Get the user based off username
        [HttpGet("{id}")]
        public IActionResult GetUser(int Id)
        {
            try
            {
                return Ok(UserLogic.getUser(Id));
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

        //Change user on the DB
        [HttpPut("{id}")]
        public IActionResult ChangeUser(int Id, [FromBody] User user)
        {
            try
            {
                return Ok(UserLogic.changeUser(Id, user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Delete user from the DB
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromBody] User user)
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
