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


//NOTES:
//Sponsor users can call
//    -> point tracking report (slect drivers) CALL DRIVER POINT
//    -> audit log report - LET DEFINE WHICH CAT AND CALL THAT FUNCTION

//admin users
//    -> sale by sponsor report CALL SALES BY SPONSOR 
//    -> invoice per sponsor CALL SALES BY SPONSOR AND THEN THE SALES BY DRIVER???
//    -> audit log report - LET DEFINE WHICH CAT AND CALL THAT FUNCTION

//audit categories
//    -> get driver apps with driver and sponsor info
//    -> get all point changes
//    -> get password changes by user
//    -> login attempts by username

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

        //get individual driver point transaction by date
        [HttpGet("GetDriverPointTransactions")]
        public async Task<List<PointTransaction>> GetDriverPointTransactionsAsync(DateTime? start, DateTime? end, int driverID)
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

        //get all applications to a sponsor
        [HttpGet("GetSponsorsApplications")]
        public async Task<ActionResult<List<Application>>> GetSponsorsApplicationsAsync(int sponsorID)
        {
            var serviceProvider = HttpContext.RequestServices;
            var ApplicationControllerInstance = serviceProvider.GetRequiredService<ApplicationController>();

            ActionResult<List<Application>> applications = await ApplicationControllerInstance.GetApplicationsBySponsor(sponsorID);

            return applications;
        }

        //get all pwd attempts by user
        //[HttpGet("GetPassChanges")]
        //public async Task<List<Application>> GetPassAttemptsAsync(int userID)
        //{
        //    var serviceProvider = HttpContext.RequestServices;
        //    var ApplicationControllerInstance = serviceProvider.GetRequiredService<ApplicationController>();

        //    ActionResult<List<Application>> passChanges = await ApplicationControllerInstance.GetApplicationsBySponsor(sponsorID);

        //    return passChanges;
        //}

        //get all login attempts by user
        [HttpGet("GetLoginAttempts")]
        public async Task<ActionResult<List<LoginAttempt>>> GetLoginAttempts(string username)
        {
            var serviceProvider = HttpContext.RequestServices;
            var LoginAttemptControllerInstance = serviceProvider.GetRequiredService<LoginAttemptController>();

            ActionResult<List<LoginAttempt>> loginChanges = await LoginAttemptControllerInstance.GetLoginAttempts(username);

            return loginChanges;
        }

        //Get sales for an indivual sponsor
        [HttpGet("GetSalesBySponsor")]
        public async Task<List<DriverOrder>> GetSalesBySponsorAsync(int SponsorID, DateTime? start, DateTime? end)
        {
            var serviceProvider = HttpContext.RequestServices;
            var DriverOrderControllerInstance = serviceProvider.GetRequiredService<DriverOrderController>();

            List<DriverOrder> driverOrders = await DriverOrderControllerInstance.GetAllBySponsorAsync(SponsorID);

            //whittle away driver orders before start date
            if (start != null)
            {
                foreach ( DriverOrder d in driverOrders)
                {
                    if (DateTime.Compare(d.OrderDate, (DateTime)start) < 0)
                        driverOrders.Remove(d);
                }
            }

            //whittle away driver orders after end date
            if (end != null)
            {
                foreach (DriverOrder d in driverOrders)
                {
                    if (DateTime.Compare(d.OrderDate, (DateTime)end) > 0)
                        driverOrders.Remove(d);
                }
            }

            return driverOrders;
        }

        //Get sales for an indivual driver
        [HttpGet("GetSalesByDriver")]
        public async Task<List<DriverOrder>> GetSalesByDriverAsync(int DriverID, DateTime? start, DateTime? end)
        {
            var serviceProvider = HttpContext.RequestServices;
            var DriverOrderControllerInstance = serviceProvider.GetRequiredService<DriverOrderController>();

            List<DriverOrder> driverOrders = await DriverOrderControllerInstance.GetAllBySponsorAsync(DriverID);

            //remove driver orders before start date
            if (start != null)
            {
                foreach (DriverOrder d in driverOrders)
                {
                    if (DateTime.Compare(d.OrderDate, (DateTime)start) < 0)
                        driverOrders.Remove(d);
                }
            }

            //remove driver orders after end date
            if (end != null)
            {
                foreach (DriverOrder d in driverOrders)
                {
                    if (DateTime.Compare(d.OrderDate, (DateTime)end) > 0)
                        driverOrders.Remove(d);
                }
            }

            return driverOrders;
        }
    }

}

