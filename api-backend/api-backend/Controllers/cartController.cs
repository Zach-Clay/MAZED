using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazedDB.Data;
using MazedDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifySharp;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class cartController : Controller
    {
        private readonly MazedDBContext _context;

        public cartController(MazedDBContext context)
        {
            _context = context;
        }

        private bool CartExists(int id)
        {
            return (_context.DriverCarts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: api/values
        [HttpGet]
        //get all carts
        public async Task<ActionResult<IEnumerable<DriverCart>>> GetCartProds()
        {
            if (_context.DriverCarts == null)
            {
                return NotFound();
            }
            return await _context.DriverCarts.ToListAsync();
        }

        // GET api/values/5
        //get a specific cart - will show every value that the cart has 
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverCart>> GetUsersCartProds(int id)
        {
            if (_context.DriverCarts == null) return NotFound();

            var cart = await _context.DriverCarts.FindAsync(id) ?? throw new Exception("Cart not found");

            return cart;
        }


        //after calling the cart by user ID
        [HttpGet("GetCartByUser/{userId}")]
        public async Task<List<DriverCart>> GetCartByUser(int userId)
        {
            return await _context.DriverCarts.Where(p => p.UserId == userId).ToListAsync();
        }

        [HttpGet("GetCartByUserSponsorTrackId")]
        public async Task<DriverCart> GetCartByUserSponsorTrackId(int userId, int sponsorId, int productId)
        {
            List<DriverCart> cart = await _context.DriverCarts.Where(p => p.UserId == userId && p.SponsorId == sponsorId && p.ProductId == productId).ToListAsync();
            return cart.ElementAt(0);
        }

        //use this if want all information from product id -> can call this with delete for product deletion
        [HttpGet("GetCartProducts/{productId}")]
        public async Task<List<DriverCart>> GetCartProducts(int productId)
        {
            return await _context.DriverCarts.Where(p => p.ProductId == productId).ToListAsync();
        }

        // POST api/values
        //need to post to certain user's cart
        [HttpPost]
        public async Task<ActionResult<DriverCart>> PostCartProd([FromBody]DriverCart cartProd)
        {
            if (_context.DriverCarts == null)
            {
                return Problem("Entity set 'MazedDBContext.DriverCarts'  is null.");
            }


            _context.DriverCarts.Add(cartProd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartProds", new { productId = cartProd.ProductId }, cartProd);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, DriverCart cart)
        {
            if (id != cart.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // DELETE api/values/5
        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.DriverCarts == null) return NotFound();

            var product = await _context.DriverCarts.FindAsync(id);
            if(product == null){ 
                return NotFound();
            }

            _context.DriverCarts.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteByUser/{userId}")]
        public async Task<IActionResult> DeleteCartByUser(int userId)
        {
            if (_context.DriverCarts == null) return NotFound();

            var serviceProvider = HttpContext.RequestServices;
            var driverOrderController = serviceProvider.GetRequiredService<DriverOrderController>();

            List<DriverCart> cart = await GetCartByUser(userId);

            await driverOrderController.PostDriverOrderAsync(cart);

            //remove all products in cart based off user
            foreach (DriverCart product in cart)
            {
                _context.DriverCarts.Remove(product);
                await _context.SaveChangesAsync();
            }
            

            return NoContent();
        }
    }
}

