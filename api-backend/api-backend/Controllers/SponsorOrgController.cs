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

        [HttpGet("GetSponsorOrgDailyPointValue/{id}")]
        public async Task<double> GetSponsorOrgDailyPointValue(int id)
        {
            if (_context.SponsorOrgs == null)
            {
                throw new Exception("Sponsors not found");
            }

            SponsorOrg? org = await _context.SponsorOrgs.Where(s => s.Id == id).FirstOrDefaultAsync()
                  ?? throw new Exception("Sponsor does not exist or Daily amount is not set");

            return org.DailyPointAmount;
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

        [HttpGet("GetSponsorOrg_Object")]
        public async Task<SponsorOrg> GetSponsorOrg_Object(int id)
        {
            if (_context.SponsorOrgs == null)
            {
                throw new Exception("Sponsors not found");
            }
            var sponsorOrg = await _context.SponsorOrgs.FindAsync(id);

            if (sponsorOrg == null)
            {
                throw new Exception("Sponsor not found");
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
