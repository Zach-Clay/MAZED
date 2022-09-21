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
        public async Task<IActionResult> AddUsers(AddUsersRequest addUsers)
        {
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