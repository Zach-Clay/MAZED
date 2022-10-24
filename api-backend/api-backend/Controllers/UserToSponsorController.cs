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
using User = MazedDB.Models.User;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_backend.Controllers
{
    [Route("api/userToSponsor")]
    [ApiController]
    public class UserToSponsorController : Controller
    {
        private readonly MazedDBContext _context;
        //private readonly MazedDBContextProcedures _contextProcedures;

        public UserToSponsorController(MazedDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserPointsBySponsor")]
        public async Task<UserToSponsor> GetUserPointsBySponsor(uint userID, uint sponsorID)
        {
            var uts = await _context.UserToSponsors.Where(u => u.UserId == userID && u.SponsorId == sponsorID).FirstOrDefaultAsync()
                ?? throw new Exception("could not find user");
            return uts;
        }

        //get all users by a sponsor'sId
        [HttpGet("GetSponsorsBySponsorId/{SponsorId}")]
        public async Task<List<User>> GetSponsorsBySponsorId(int SponsorId)
        {
            var serviceProvider = HttpContext.RequestServices;
            var userControllerInstance = serviceProvider.GetRequiredService<UserController2>();

            List<UserToSponsor> userToSponsors = await _context.UserToSponsors.Where(p => p.SponsorId == SponsorId).ToListAsync();

            List<User> users = new List<User>();
            foreach (var userToSponsor in userToSponsors)
            {
                if (userToSponsor.UserType.ToLower() == "driver") continue;
                var user = await userControllerInstance.GetUserById_Object((int)userToSponsor.UserId);
                users.Add(user);
            }

            return users;
        }

        //get all users by a sponsor'sId
        [HttpGet("GetDriversBySponsorId/{SponsorId}")]
        public async Task<List<User>> GetDriversBySponsorId(int SponsorId)
        {
            var serviceProvider = HttpContext.RequestServices;
            var userControllerInstance = serviceProvider.GetRequiredService<UserController2>();

            List<UserToSponsor> userToSponsors = await _context.UserToSponsors.Where(p => p.SponsorId == SponsorId).ToListAsync();

            List<User> users = new List<User>();
            foreach (var userToSponsor in userToSponsors)
            {
                if (userToSponsor.UserType.ToLower() == "sponsor") continue;
                var user = await userControllerInstance.GetUserById_Object((int)userToSponsor.UserId);
                users.Add(user);
            }

            return users;
        }

        //loading related data***
        [HttpGet("GetSponsorsFromUserId/{Id}")]
        public async Task<List<UserToSponsor>> GetSponsorsFromUserId(int Id)
        {
            return await _context.UserToSponsors.Where(p => p.SponsorId == Id).ToListAsync();
        }

        //loading related data***
        [HttpGet("GetSponsorsOrgsFromUserId/{Id}")]
        public async Task<List<SponsorOrg>> GetSponsorsOrgsFromUserId(int Id)
        {
            List<UserToSponsor> userToSponsors =  await _context.UserToSponsors.Where(p => p.SponsorId == Id).ToListAsync();

            var serviceProvider = HttpContext.RequestServices;
            var sponsorOrgControllerInstance = serviceProvider.GetRequiredService<SponsorOrgController>();

            List<SponsorOrg> sponsorOrgs = new List<SponsorOrg>();
            foreach (var userToSponsor in userToSponsors)
            {
                var sponsorOrg = await sponsorOrgControllerInstance.GetSponsorOrg_Object((int)userToSponsor.Id);
                sponsorOrgs.Add(sponsorOrg);
            }

            return sponsorOrgs;
        }

        //get all drivers by a sponsor'sId
        [HttpGet("/GetDriversBySponsorId/{SponsorId}")]
        public async Task<List<UserToSponsor>> GetDriversBySponsorId(int SponsorId)
        {
            return await _context.UserToSponsors.Where(u => u.SponsorId == SponsorId && u.UserType.ToLower() == "driver").ToListAsync();
        }


        [HttpPut("UpdateUserPointsBySponsor/{amount}")]
        public async Task<UserToSponsor> UpdateUserPointsBySponsor(uint userID, uint sponsorID, double amount)
        {
            var user = await GetUserPointsBySponsor(userID, sponsorID) ?? throw new Exception("user or sponsor not found");

            //telling context the entry was modified so we then can change it
            _context.Entry(user).State = EntityState.Modified;

            user.UserPoints += amount;

            await _context.SaveChangesAsync();

            return user;
        }

        //loading related data***
        [HttpGet("/GetSponsorFromUserId/{Id}")]
        public async Task<UserToSponsor?> GetSponsorFromUserId(int id)
        {
            return await _context.UserToSponsors.Include(p => p.SponsorId).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet("/GetDriverPoints/{Id}")]
        public async Task<List<UserToSponsor>> GetDriverPoints(int id)
        {
            return await _context.UserToSponsors.Where(u => u.Id == id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserToSponsor>> PostUserToSponsor(UserToSponsor user)
        {
            if (_context.UserToSponsors == null)
            {
                return Problem("Entity set 'MazedDBContext.UserToSponsors'  is null.");
            }

            _context.UserToSponsors.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserPointsBySponsor", new { id = user.Id }, user);
        }
    }
}

