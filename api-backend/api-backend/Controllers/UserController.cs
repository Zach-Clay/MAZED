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


        //Get the user based off id
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                return Ok(UserLogic.getUser(id));
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
        public IActionResult ChangeUser(int id, [FromBody] User user)
        {
            try
            {
                return Ok(UserLogic.changeUser(id, user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Delete user from the DB with id = id
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                return Ok(UserLogic.deleteUser(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //user log in user from the DB
        [HttpPost("login")]
        public IActionResult UserLogin([FromBody] User user)
        {
            try
            {
                //return Ok(UserLogic.userLogin(user));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
