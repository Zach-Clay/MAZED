using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazedDB.Data;
using MazedDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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

            return await _context.Products.ToListAsync<Product>();
        }

        // GET: api/ProductController
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null) return NotFound();

            var product = await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MazedDBContext.Products'  is null.");
            }


            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { productId = product.ProductId }, product);
        }

        [HttpPost("PostArrayOfTrackIds")]
        public async Task<ActionResult> PostArrayOfTrackIds(int sponsorId, List<int> trackIds)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MazedDBContext.Products'  is null.");
            }

            List<Product> products =  await _context.Products.Where(p => p.SponsorId == sponsorId).ToListAsync();            

            List<int> currIds = new List<int>();
            foreach (var item in products) {
                currIds.Add(item.TrackId);
            }

            foreach(int id in trackIds)
            {
                //account for duplicates
                if (currIds.Contains(id)) continue;

                Product product = new Product();
                product.ProductId = 0;
                product.SponsorId = sponsorId;
                product.OrderId = null;
                product.TrackId = id;
                _context.Products.Add(product);
            }

            //_context.Products.Add()
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null) return NotFound();

            var product = await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("DeleteByArrayOfTrackIds")]
        public async Task<IActionResult> DeleteProductByArrayOfTrackIds(int sponsorId, List<int> trackIds)
        {
            if (_context.Products == null) return NotFound();

            List<Product> products = await _context.Products.Where(p => p.SponsorId == sponsorId).ToListAsync();

            foreach (var product in products)
            {
                if (trackIds.Contains(product.TrackId))
                {
                    _context.Products.Remove(product);
                }
            }

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

        //get all products by a sponsor'sId
        //used this one and then call using track ID for getting catlogue sponsor info
        [HttpGet("GetProductsBySponsorId/{SponsorId}")]
        public async Task<ActionResult<List<Product>>> GetProductsBySponsorId(int SponsorId)
        {
            return await _context.Products.Where(p => p.SponsorId == SponsorId).ToListAsync();
        }

        //get all products by a sponsor'sId
        [HttpGet("GetiTunesProductsBySponsorId/{SponsorId}")]
        public async Task<ActionResult<JValue>> GetiTunesProductsBySponsorId(int SponsorId)
        {
            List<Product> products = await _context.Products.Where(p => p.SponsorId == SponsorId).ToListAsync();

            var serviceProvider = HttpContext.RequestServices;
            var sponsorParamsQueryInstance = serviceProvider.GetRequiredService<SponsorQueryParamsController>();

            string iTunesItems = "[";

            foreach (Product product in products)
            {
                string trackId = product.TrackId.ToString();
                string term = "all";
                string? iTunesItem = await sponsorParamsQueryInstance.GetMediaTerm(trackId, term);
                if (iTunesItem != null)
                {
                    iTunesItems+=iTunesItem;
                }
            }

            iTunesItems += "]";
            return (JValue)iTunesItems;
        }

    }
}

