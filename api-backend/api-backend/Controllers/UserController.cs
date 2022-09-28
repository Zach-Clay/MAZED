using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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

        //Get all of the users based off id
        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(UserLogic.getAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Get the user based off id
        [HttpGet("{username}")]
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

        //Register user to the DB
        [HttpPost]
        public IActionResult AddUser([FromBody]User user)
        {
            try
            {
                return Ok(UserLogic.registerUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Change user on the DB
        [HttpPut("{id}")]
        public IActionResult ChangeUserWId(int id, [FromBody] User user)
        {
            try
            {
                return Ok(UserLogic.changeUserWID(id, user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Change user on the DB
        [HttpPut("{username}")]
        public IActionResult ChangeUserWUsername(string username, [FromBody] User user)
        {
            try
            {
                return Ok(UserLogic.changeUserWUsername(username, user));
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

        ////user log in user from the DB
        //[HttpPost("login")]
        //public IActionResult UserLogin([FromBody] user user)
        //{
        //    try
        //    {
        //        //return Ok(UserLogic.userLogin(user));
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

    }
}
