using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazedDB.Data;
using MazedDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly MazedDBContext _context;
        //private readonly MazedDBContextProcedures _contextProcedures;

        public ProductController(MazedDBContext context)
        {
            _context = context;
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            if (_context.Products == null) return NotFound();

            return await _context.Products.Where(p => p.IsBlacklisted == 0).ToListAsync<Product>();
        }

        // GET: api/ProductController
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null) return NotFound();

            var product = await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");

            if (product.IsBlacklisted == 1) return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostUser(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MazedDBContext.Products'  is null.");
            }


            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { productId = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null) return NotFound();

            var product = await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");

            product.IsBlacklisted = 1;
            //telling context the entry was modified so we then can change it
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

    }
}

