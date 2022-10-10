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
    [Route("api/user")]
    [ApiController]
    public class UserController2 : ControllerBase
    {
        private readonly MazedDBContext _context;
        //private readonly MazedDBContextProcedures _contextProcedures;

        public UserController2(MazedDBContext context)
        {
            _context = context;
            //_contextProcedures = contextProcedures;
        }

        // GET: api/UserController2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null) return NotFound();
            
            return await _context.Users.Where(e => e.IsBlacklisted == 0).ToListAsync();
        }

        // GET: api/UserController2/5
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            if (_context.Users == null) return NotFound();

            var user = await _context.Users.Where(e => e.Username == username).ToListAsync();

            if (user.Count < 1) return NotFound();
            if (user.ElementAt(0).IsBlacklisted == 1) return NotFound();

            return user.ElementAt(0);
        }

        // GET: api/UserController2/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserId(int id)
        {
            if (_context.Users == null) return NotFound();

            var user = await _context.Users.Where(e => e.Id == id).ToListAsync();

            if (user.Count < 1) return NotFound();
            if (user.ElementAt(0).IsBlacklisted == true) return NotFound();

            return user.ElementAt(0);
        }

        // PUT: api/UserController2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id) return BadRequest();
            
            //telling context the entry was modified so we then can change it
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //change user based on their username
        [HttpPut("{username}/change by username")]
        public async Task<IActionResult> PutUserUsername(string username, User user)
        {
            if (username != user.Username) return BadRequest();

            //telling context the entry was modified so we then can change it
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExistsUsername(username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //user this for username PUT
        private bool UserExistsUsername(string username)
        {
            throw new NotSupportedException();
        }

        // POST: api/UserController2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MazedDBContext.Users'  is null.");
            }

            user.ModDate = DateTime.Now;
            user.ModBy = user.Username;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { username = user.Username }, user);
        }

        // DELETE: api/UserController2/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null) return NotFound();
            
            var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");

            user.IsBlacklisted = 1;
            //telling context the entry was modified so we then can change it
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //loading related data***
        [HttpGet("GetSponsorFromUserId")]
        public async Task<User?> GetSponsorFromUserId(int id)
        {
            return await _context.Users.Include(p => p.SponsorId).Where(p => p.Id == id && p.IsBlacklisted == 0).FirstOrDefaultAsync();
        }

        //how to call stored proceduere

        //get all drivers by a sponsor'sId
        [HttpGet("GetDriversBySponsorId")]
        public async Task<List<User>> GetDriversBySponsorId(int SponsorId)
        {
            return await _context.Users.Where(u => u.SponsorId == SponsorId).ToListAsync();
        }

    }
}
