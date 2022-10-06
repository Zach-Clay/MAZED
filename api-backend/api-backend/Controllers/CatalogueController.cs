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
    public class CatalogueController : ControllerBase
    {
        private readonly MazedDBContext _context;

        public CatalogueController(MazedDBContext context)
        {
            _context = context;
        }

        // GET: api/Catalogue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalogue>>> GetCatalogues()
        {
          if (_context.Catalogues == null)
          {
              return NotFound();
          }
            return await _context.Catalogues.ToListAsync();
        }

        // GET: api/Catalogue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Catalogue>> GetCatalogue(int id)
        {
          if (_context.Catalogues == null)
          {
              return NotFound();
          }
            var catalogue = await _context.Catalogues.FindAsync(id);

            if (catalogue == null)
            {
                return NotFound();
            }

            return catalogue;
        }

        // PUT: api/Catalogue/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogue(int id, Catalogue catalogue)
        {
            if (id != catalogue.Id)
            {
                return BadRequest();
            }

            _context.Entry(catalogue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogueExists(id))
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

        // POST: api/Catalogue
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Catalogue>> PostCatalogue(Catalogue catalogue)
        {
          if (_context.Catalogues == null)
          {
              return Problem("Entity set 'MazedDBContext.Catalogues'  is null.");
          }
            _context.Catalogues.Add(catalogue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalogue", new { id = catalogue.Id }, catalogue);
        }

        // DELETE: api/Catalogue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogue(int id)
        {
            if (_context.Catalogues == null)
            {
                return NotFound();
            }
            var catalogue = await _context.Catalogues.FindAsync(id);
            if (catalogue == null)
            {
                return NotFound();
            }

            _context.Catalogues.Remove(catalogue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogueExists(int id)
        {
            return (_context.Catalogues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
