using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using api_backend.Data;
using api_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        //how we talk to db
        private readonly UsersAPIDBContext dbContext;

        public Guid UserId { get; private set; }

        public UsersController(UsersAPIDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get keyword but for swagger we need this annotation
        [HttpGet]
        //talk to database async
        public async Task<IActionResult> GetUsers()
        {
            //inject DB context, wrap for response
            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers(AddUsersRequest addUsers)
        {
            var user = new users()
            {
                UserId = Guid.NewGuid(),
                SponsorID = Guid.NewGuid(),
                FName = addUsers.FName,
                LName = addUsers.LName,
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
        }
    }
}