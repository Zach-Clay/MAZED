using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazedDB.Data;
using MazedDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverOrderController : Controller
    {
        private readonly MazedDBContext _context;

        public DriverOrderController(MazedDBContext context)
        {
            _context = context;
        }

        [HttpGet("Get/{id}")]
        public async Task<DriverOrder> GetDriverOrderAsync(int id)
        {
            return await _context.DriverOrders.FindAsync(id) ?? throw new Exception("Order not found");
        }

        [HttpGet]
        public async Task<List<DriverOrder>> GetAllAsync()
        {
            return await _context.DriverOrders.ToListAsync() ?? throw new Exception("Order not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.DriverOrders == null) return NotFound();

            var order = await _context.DriverOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.DriverOrders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetOrdersByDriverId/{id}")]
        public async Task<List<DriverOrder>> GetOrdersByDriverId(int id)
        {
            return await _context.DriverOrders.Where(o => o.UserId == id).ToListAsync();
        }

        //take cart and make it into an order
        //this will be only be called by cartController
        [HttpPost("Post")]
        public async Task<DriverOrder> PostDriverOrderAsync(List<DriverCart> cart)
        {
            //this is assuming all the items in the cart are for the same user and sponsor
            int id = cart[0].UserId;
            int sponsorId = cart[0].SponsorId;
            double totalPoints = 0.0;

            foreach (DriverCart cartProduct in cart) totalPoints += cartProduct.PointValue;

            //how to come up with unique number
            DriverOrder toBePosted = new DriverOrder
            {
                UserId = id,
                SponsorId = sponsorId,
                TotalPointVal = totalPoints,
                OrderStatus = "Processing",
                OrderDate = DateTime.Now,
                ProductList = JsonSerializer.Serialize(cart)
            };

            _context.DriverOrders.Add(toBePosted);
            await _context.SaveChangesAsync();

            return toBePosted;

        }
    }
}

