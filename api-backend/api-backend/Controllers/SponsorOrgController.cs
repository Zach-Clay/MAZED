using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SponsorOrgController : ControllerBase
    {
        private readonly MazedDBContext _context;

        public SponsorOrgController(MazedDBContext context)
        {
            _context = context;
        }

        // GET: api/SponsorOrg
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SponsorOrg>>> GetSponsorOrgs()
        {
          if (_context.SponsorOrgs == null)
          {
              return NotFound();
          }
            return await _context.SponsorOrgs.ToListAsync();
        }

        // GET: api/SponsorOrg/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SponsorOrg>> GetSponsorOrg(int id)
        {
          if (_context.SponsorOrgs == null)
          {
              return NotFound();
          }
            var sponsorOrg = await _context.SponsorOrgs.FindAsync(id);

            if (sponsorOrg == null)
            {
                return NotFound();
            }

            return sponsorOrg;
        }

        // PUT: api/SponsorOrg/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSponsorOrg(int id, SponsorOrg sponsorOrg)
        {
            if (id != sponsorOrg.Id)
            {
                return BadRequest();
            }

            _context.Entry(sponsorOrg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorOrgExists(id))
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

        // POST: api/SponsorOrg
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SponsorOrg>> PostSponsorOrg(SponsorOrg sponsorOrg)
        {
          if (_context.SponsorOrgs == null)
          {
              return Problem("Entity set 'MazedDBContext.SponsorOrgs'  is null.");
          }
            _context.SponsorOrgs.Add(sponsorOrg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSponsorOrg", new { id = sponsorOrg.Id }, sponsorOrg);
        }

        // DELETE: api/SponsorOrg/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsorOrg(int id)
        {
            if (_context.SponsorOrgs == null)
            {
                return NotFound();
            }
            var sponsorOrg = await _context.SponsorOrgs.FindAsync(id);
            if (sponsorOrg == null)
            {
                return NotFound();
            }

            _context.SponsorOrgs.Remove(sponsorOrg);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SponsorOrgExists(int id)
        {
            return (_context.SponsorOrgs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("GetDollarToPoint/{id}")]
        public async Task<double?> GetDollarToPointBySponsorId(int id)
        {
            SponsorOrg? sponsorOrg = await _context.SponsorOrgs.FindAsync(id);
            return sponsorOrg?.DollarToPoint;
        }
    }
}
