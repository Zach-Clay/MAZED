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
    public class PointTransController : ControllerBase
    {
        private readonly MazedDBContext _context;

        public PointTransController(MazedDBContext context)
        {
            _context = context;
        }

        // GET: api/PointTrans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointTransaction>>> GetPointTransactions()
        {
          if (_context.PointTransactions == null)
          {
              return NotFound();
          }
            return await _context.PointTransactions.ToListAsync();
        }

        // GET: api/PointTrans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PointTransaction>> GetPointTransID(int id)
        {
            if (_context.Users == null) return NotFound();

            var pointTrans = await _context.PointTransactions.Where(e => e.PointId == id).ToListAsync();

            return pointTrans.ElementAt(0);
        }

        // PUT: api/PointTrans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPointTransaction(int id, PointTransaction pointTransaction)
        {
            if (id != pointTransaction.PointId)
            {
                return BadRequest();
            }

            _context.Entry(pointTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointTransactionExists(id))
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

        // POST: api/PointTrans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PointTransaction>> PostPointTransaction(PointTransaction pointTransaction)
        {
          if (_context.PointTransactions == null)
          {
              return Problem("Entity set 'MazedDBContext.PointTransactions'  is null.");
          }
            pointTransaction.ModDate = DateTime.Now;
            _context.PointTransactions.Add(pointTransaction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PointTransactionExists(pointTransaction.PointId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPointTransID", new { id = pointTransaction.PointId }, pointTransaction);
        }

        // DELETE: api/PointTrans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePointTransaction(int id)
        {
            if (_context.PointTransactions == null)
            {
                return NotFound();
            }
            var pointTransaction = await _context.PointTransactions.FindAsync(id);
            if (pointTransaction == null)
            {
                return NotFound();
            }

            _context.PointTransactions.Remove(pointTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PointTransactionExists(int id)
        {
            return (_context.PointTransactions?.Any(e => e.PointId == id)).GetValueOrDefault();
        }

        //get point history for a user based on username

        [HttpGet("GetPointsForUser/{UserId}")]
        public async Task<List<PointTransaction>> GetAllPointsForUser(int UserId)
        {
            return await _context.PointTransactions.Where(p => p.UserId == UserId).ToListAsync();
        }
    }
}
