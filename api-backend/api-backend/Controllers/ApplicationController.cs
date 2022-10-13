using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MazedDB.Data;
using MazedDB.Models;
using static api_backend.Controllers.UserController2;

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Application>> Post(Application application)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'MazedDBContext.Applications'  is null.");
            }

            application.RequestedDate = DateOnly.FromDateTime(DateTime.Now);
            application.ResponseDate = DateOnly.FromDateTime(DateTime.MinValue);
            application.IsActive = 1;
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> Put(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            if (application.IsActive == 0)
            {
                application.ResponseDate = DateOnly.FromDateTime(DateTime.Now);
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

