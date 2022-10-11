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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    public class LoginAttemptController : Controller
    {
        private readonly MazedDBContext _context;
        //private readonly MazedDBContextProcedures _contextProcedures;

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

        //Post a new login attempt for a specific user
        [HttpPost]
        public async Task<ActionResult<LoginAttempt>> Post([FromBody]LoginAttempt attempt)
        {
            if (_context.LoginAttempts == null)
            {
                return Problem("Entity set 'MazedDBContext.LoginAttemps'  is null.");
            }

            attempt.AttemptedDate = DateTime.Now;
            _context.LoginAttempts.Add(attempt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoginAttempts", new { username = attempt.Username }, attempt);
        }
    }
}

