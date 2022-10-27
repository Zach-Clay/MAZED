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

        [HttpGet("GetTotalUserPoints")]
        public async Task<double> GetTotalUserPoints(uint userID)
        {
            List<UserToSponsor> uts = await _context.UserToSponsors.Where(u => u.UserId == userID).ToListAsync();

            double total = 0;
            foreach (var u in uts)
            {
                total += u.UserPoints;
            }

            return total;
        }
        

        //get all sponsors by a sponsor's Id
        [HttpGet("GetSponsorsBySponsorId/{SponsorOrgId}")]
        public async Task<List<User>> GetSponsorsBySponsorId(int SponsorOrgId)
        {
            var serviceProvider = HttpContext.RequestServices;
            var userControllerInstance = serviceProvider.GetRequiredService<UserController2>();

            List<UserToSponsor> userToSponsors = await _context.UserToSponsors.Where(p => p.SponsorId == SponsorOrgId).ToListAsync();

            List<User> users = new List<User>();
            foreach (var userToSponsor in userToSponsors)
            {
                if (userToSponsor.UserType.ToLower() == "driver") continue;
                var user = await userControllerInstance.GetUserById_Object((int)userToSponsor.UserId);
                users.Add(user);
            }

            return users;
        }

        //get all drivers by a sponsor'sId
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

        //Get all sponsor orgs from a driver's ID
        [HttpGet("GetSponsorsOrgsFromDriverUsersId/{DriverUsersId}")]
        public async Task<List<SponsorOrg>> GetSponsorsOrgsFromDriverId(int DriverUsersId)
        {
            List<UserToSponsor> userToSponsors = await _context.UserToSponsors.Where(p => p.UserId == DriverUsersId).ToListAsync();

            var serviceProvider = HttpContext.RequestServices;
            var sponsorOrgControllerInstance = serviceProvider.GetRequiredService<SponsorOrgController>();

            List<SponsorOrg> sponsorOrgs = new List<SponsorOrg>();
            foreach (var userToSponsor in userToSponsors)
            {
                var sponsorOrg = await sponsorOrgControllerInstance.GetSponsorOrg_Object((int)userToSponsor.SponsorId);
                sponsorOrgs.Add(sponsorOrg);
            }

            return sponsorOrgs;
        }

        //Get the sponsor org for a given sponsor ID --> each sponsor can only have one sponsor org!
        [HttpGet("GetSponsorOrgFromSponsorUsersId/{SponsorUserId}")]
        public async Task<SponsorOrg> GetSponsorsOrgsFromSponsorUsersId(int SponsorUserId)
        {
            List<UserToSponsor> userToSponsors = await _context.UserToSponsors.Where(p => p.UserId == SponsorUserId).ToListAsync();

            var serviceProvider = HttpContext.RequestServices;
            var sponsorOrgControllerInstance = serviceProvider.GetRequiredService<SponsorOrgController>();

            List<SponsorOrg> sponsorOrgs = new List<SponsorOrg>();
            foreach (var userToSponsor in userToSponsors)
            {
                if (userToSponsor.UserType.ToLower() == "driver") continue;
                var sponsorOrg = await sponsorOrgControllerInstance.GetSponsorOrg_Object((int)userToSponsor.SponsorId);
                sponsorOrgs.Add(sponsorOrg);
            }

            if (sponsorOrgs.Count > 1)
            {
                throw new Exception("Sponsor user cannot have more than one sponsor org");
            }

            return sponsorOrgs[0];
        }


        //loading related data***
        [HttpGet("GetSponsorsEntriesFromUserId/{Id}")]
        public async Task<List<UserToSponsor>> GetSponsorsFromUserId(int Id)
        {
            return await _context.UserToSponsors.Where(p => p.SponsorId == Id).ToListAsync();
        }

        [HttpGet("GetDriversEntriesFromDriverUsersId/{DriverUsersId}")]
        public async Task<List<UserToSponsor>> GetDriversFromUserId(int DriverUsersId)
        {
            return await _context.UserToSponsors.Where(p => p.UserId == DriverUsersId).ToListAsync();
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

        [HttpDelete("{Id}")]
        public async Task<ActionResult> RemoveUserFromSponsor(int Id)
        {
            if (_context.UserToSponsors == null)
            {
                return Problem("Entity set 'MazedDBContext.UserToSponsors'  is null.");
            }

            List<UserToSponsor> entries = await _context.UserToSponsors.Where(p => p.Id == Id).ToListAsync();

            _context.UserToSponsors.Remove(entries[0]);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

