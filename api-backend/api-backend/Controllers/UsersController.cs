using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using api_backend.Data;
using api_backend.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

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
            //return Ok(dbcontext.Users.ToList());

            using var connection = new MySqlConnection("Server=team2.codb8enwsupz.us-east-1.rds.amazon.aws; Database=TEAM_DB; ID=admin; Password=cpsc4910;");
            connection.Open();

            using var command = new MySqlCommand("SELECT * FROM TEAM_DB.users;", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine(reader.GetString(0));


            return Ok("hello");

        }

        [HttpPost]
        public IActionResult AddUsers(AddUsersRequest addUsers)
        {
    //        var users = new Users()
    //        {
    //            users.UserId = Guid.NewGuid(),
    //            users.FName = addUsers
    //}

            return Ok("Not Implemented");
        }
    }
}