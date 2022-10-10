using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ApplicationController : Controller
    {
        private readonly MazedDBContext _context;

        public ApplicationController(MazedDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            if (_context.Applications == null)
            {
                return NotFound();
            }
            var applications = await _context.Applications.Where(e => e.Id == id).ToListAsync();

            return applications.ElementAt(0);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<Application>>> GetApplicationsByUser(int id)
        {
            if (_context.Applications == null)
            {
                return NotFound();
            }
            var applications = await _context.Applications.Where(e => e.UserId == id).ToListAsync();

            return applications;
        }

        [HttpGet("sponsor/{id}")]
        public async Task<ActionResult<List<Application>>> GetApplicationsBySponsor(int id)
        {
            if (_context.Applications == null)
            {
                return NotFound();
            }
            var applications = await _context.Applications.Where(e => e.SponsorId == id).ToListAsync();

            return applications;
        }

        [HttpPost]
        public async Task<ActionResult<Application>> Post([FromBody] Application application)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'MazedDBContext.PointTransactions'  is null.");
            }

            application.RequestedDate = DateOnly.FromDateTime(DateTime.Now);
            application.ResponseDate = null;
            application.IsActive = 1;
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> Put(int id, [FromBody] Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return application;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

