using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using api_backend.Data;
using api_backend.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using api_backend.Logic;
using static Org.BouncyCastle.Math.EC.ECCurve;

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

    }
}
