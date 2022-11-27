using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MazedDB.Data;
using MazedDB.Models;

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    public class AuditController : Controller
    {
        private readonly MazedDBContext _context;

        public AuditController(MazedDBContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        //get all admin logs?
        //public async Task<ActionResult<IEnumerable<AuditLogging>>> getAdminLog()
        //{

        //}

        //get individual driver point transaction by date
        [HttpGet("GetDriverPointTransactions")]
        public async Task<List<PointTransaction>> GetDriverPointTransactions(DateTime? start, DateTime? end, int driverID)
        {
            var serviceProvider = HttpContext.RequestServices;
            var PointTransControllerInstance = serviceProvider.GetRequiredService<PointTransController>();

            List<PointTransaction> pointTransactions = await PointTransControllerInstance.GetPointHistForUser(driverID);

            //whittle away point transactions before start date
            if (start != null)
            {
                foreach (PointTransaction t in pointTransactions)
                {
                    if (DateTime.Compare(t.ModDate, (DateTime)start) < 0)
                        pointTransactions.Remove(t);
                }
            }

            //whittle away point transactions after end date
            if (end != null)
            {
                foreach (PointTransaction t in pointTransactions)
                {
                    if (DateTime.Compare(t.ModDate, (DateTime)end) > 0)
                        pointTransactions.Remove(t);
                }
            }

            return pointTransactions; 
        }
    }
}

