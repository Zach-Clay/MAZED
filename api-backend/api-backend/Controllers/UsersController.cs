using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_backend.Data;
using api_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        //how we talk to db
        private readonly UsersAPIDBContext dbcontext;

        public UsersController(UsersAPIDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        //get keyword but for swagger we need this annotation
        [HttpGet]
        public IActionResult GetUsers()
        {
            //inject DB context, wrap for response
            return Ok(dbcontext.Users.ToList());
        }

        [HttpPost]
        public IActionResult AddUsers(AddUsersRequest addUsers)
        {
            var users = new Users();
            {
                users.UserId = Guid.NewGuid(),
                users.FName = addUsers.
    }

            return Ok("Not Implemented");
        }
    }
}