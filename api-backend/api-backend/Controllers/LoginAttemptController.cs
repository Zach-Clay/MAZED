using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MazedDB.Data;
using MazedDB.Models;
namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAttemptController : Controller
    {
        private readonly MazedDBContext _context;

        public LoginAttemptController(MazedDBContext context)
        {
            _context = context;
            //_contextProcedures = contextProcedures;
        }

        //Get all login attemps for a specific user
        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<List<LoginAttempt>>> GetLoginAttempts(string username)
        {
            if (_context.LoginAttempts == null) return NotFound();

            return await _context.LoginAttempts.Where(e => e.Username == username).ToListAsync();
        }

        ////Post a new login attempt for a specific user
        //[HttpPost]
        //public async Task<ActionResult<LoginAttempt>> Post([FromBody]LoginAttempt attempt)
        //{
        //    if (_context.LoginAttempts == null)
        //    {
        //        return Problem("Entity set 'MazedDBContext.LoginAttemps'  is null.");
        //    }

        //    attempt.AttemptedDate = DateTime.Now;
        //    _context.LoginAttempts.Add(attempt);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLoginAttempts", new { username = attempt.Username }, attempt);
        //}

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<LoginAttempt>>> GetAllLoginAttempt()
        {
            return await _context.LoginAttempts.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<LoginAttempt>> GetLoginAttempt(LoginAttempt login)
        {
            return await _context.LoginAttempts.FindAsync(login) ?? throw new Exception("No such login attempt exists");
        }

        [HttpPost]
        public async Task<ActionResult<LoginAttempt>> PostLoginAttempt(LoginAttempt login)
        {
            if (_context.LoginAttempts == null)
            {
                return Problem("Entity set 'MazedDBContext.LoginAttempts'  is null.");
            }

            login.AttemptedDate = DateTime.Now;
            _context.LoginAttempts.Add(login);
            await _context.SaveChangesAsync();

            //update other models related to user login
            var serviceProvider = HttpContext.RequestServices;
            var userControllerInstance = serviceProvider.GetRequiredService<UserController2>();
            var SponsorOrgControllerInstance = serviceProvider.GetRequiredService<SponsorOrgController>();
            var PointTransControllerInstance = serviceProvider.GetRequiredService<PointTransController>();
            var userToSponsorControllerInstance = serviceProvider.GetRequiredService<UserToSponsorController>();

            User user = await userControllerInstance.GetUser_Object(login.Username);
            List<UserToSponsor> userSponsorID = await userToSponsorControllerInstance.GetSponsorsFromUserId(user.Id);

            //calculate days between last login and current login
            DateTime LastLogin = user.LastLogin ?? DateTime.Now; //first time login
            DateTime CurrentLogin = DateTime.Now;

            TimeSpan ts = CurrentLogin - LastLogin;
            int days = (int) ts.TotalDays;


            foreach (UserToSponsor u in userSponsorID) //iterate through all sponsors of user
            {
                PointTransaction dailyUpdate = new()
                {
                    SponsorId = (int)u.SponsorId,
                    UserId = user.Id,                                     
                    PointValue = await SponsorOrgControllerInstance.GetSponsorOrgDailyPointValue((int)u.SponsorId) * days, //times number of days between
                    Reason = "Daily rewards",
                    ModDate = DateTime.Now
                };

                await PointTransControllerInstance.PostPointTransaction(dailyUpdate);

                await userToSponsorControllerInstance.UpdateUserPointsBySponsor((uint)user.Id, (uint)u.SponsorId,
                    await SponsorOrgControllerInstance.GetSponsorOrgDailyPointValue((int)u.SponsorId) * days);
                user.LastLogin = login.AttemptedDate;
                await userControllerInstance.PutUser(user.Id, user);
            }
            

            return CreatedAtAction("GetLoginAttempt", new { username = login.Username }, login);
        }
    }
}

