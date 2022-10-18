using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MazedDB.Data;
using MazedDB.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        }

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


            _context.LoginAttempts.Add(login);
            await _context.SaveChangesAsync();

            //update other models related to user login
            var serviceProvider = HttpContext.RequestServices;
            var userControllerInstance = (UserController2)serviceProvider.GetRequiredService<UserController2>();
            var SponsorOrgControllerInstance = (SponsorOrgController)serviceProvider.GetRequiredService<SponsorOrgController>();
            var PointTransControllerInstance = (PointTransController)serviceProvider.GetRequiredService<PointTransController>();

            User user = await userControllerInstance.GetUser_Object(login.Username);
            int userSponsorID = user.SponsorId ?? throw new Exception("No sponsor associated with user");

            SponsorOrg sponsorOrg = await SponsorOrgControllerInstance.GetSponsorOrg_Object(userSponsorID);

            PointTransaction dailyUpdate = new PointTransaction
            {
                SponsorId = userSponsorID, 
                UserId = user.Id,                                       //hardcoded for testing
                PointValue = await SponsorOrgControllerInstance.GetSponsorOrgDailyPointValue(2), //times number of days between
                Reason = "Daily rewards",
                ModDate = DateTime.Now
            };

            await PointTransControllerInstance.PostPointTransaction(dailyUpdate);
            user.TotalPoints += await SponsorOrgControllerInstance.GetSponsorOrgDailyPointValue(2);
            user.LastLogin = login.AttemptedDate;
            await userControllerInstance.PutUser(user.Id, user);

            return CreatedAtAction("GetLoginAttempt", new { username = login.Username }, login);
        }
    }
}

