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

        [HttpGet]
        [Route("{id:guid}")]
        //talk to database async
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            //inject DB context, wrap for response
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUsers(AddUsersRequest addUsers)
        {
<<<<<<< HEAD
    //        var users = new Users()
    //        {
    //            users.UserId = Guid.NewGuid(),
    //            users.FName = addUsers
    //}

            return Ok("Not Implemented");
=======
            var user = new users()
            {
                Id = Guid.NewGuid(),
                SponsorID = Guid.NewGuid(),
                FName = addUsers.FName,
                LName = addUsers.LName,
                Username = addUsers.Username,
                UserType = addUsers.UserType,
                UserAddress = addUsers.UserAddress,
                UserEmail = addUsers.UserEmail,
                UserPhonenum = addUsers.UserPhonenum,
                UserPronouns = addUsers.UserPronouns,
                UserPwd = addUsers.UserPwd
               
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
>>>>>>> Madison/Dev
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUsers([FromRoute] Guid id, UpdateUsersRequest updateUsersRequest)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user != null)
            {
                user.SponsorID = Guid.NewGuid();
                user.FName = updateUsersRequest.FName;
                user.LName = updateUsersRequest.LName;
                user.Username = updateUsersRequest.Username;
                user.UserType = updateUsersRequest.UserType;
                user.UserAddress = updateUsersRequest.UserAddress;
                user.UserEmail = updateUsersRequest.UserEmail;
                user.UserPhonenum = updateUsersRequest.UserPhonenum;
                user.UserPronouns = updateUsersRequest.UserPronouns;
                user.UserPwd = updateUsersRequest.UserPwd;

                await dbContext.SaveChangesAsync();
                return Ok(user);
            }

            return NotFound();
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user != null)
            {
                dbContext.Remove(user);

                await dbContext.SaveChangesAsync();
                return Ok(user);
            }

            return NotFound();

        }


    }
}