using System;
using MazedDB.Data;
using MazedDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_backend.Controllers
{
    [Route("api/[controller]")]
    public class AuditController
	{
        private readonly MazedDBContext _context;

        public AuditController(MazedDBContext context)
		{
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        //get all admin logs?
        public async Task<ActionResult<IEnumerable<AuditLogging>>> getAdminLog()
        {
        }
}

