using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using api_backend.Data;
using api_backend.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace api_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        //THIS WILL BE IN EVERY CONTROLLER FILE --
        //USED TO GET THE CONNECTION STRING FROM appsettings.json
        private IConfiguration Configuration;
        public UsersController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            string connStr = this.Configuration.GetConnectionString("DB");
            MySqlConnection connection = new MySqlConnection(connStr);

            var user = new users();
            try
            {
                connection.Open();

                string sql = "SELECT * FROM TEAM2_DB.users WHERE Id=2";
                MySqlCommand cmd = new MySqlCommand(sql, connection);

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.Id = (int)rdr["Id"];
                    if (rdr["SponsorID"].ToString() != "") user.SponsorID = (int)rdr["SponsorID"];
                    user.Username = rdr["Username"].ToString();
                    user.UserType = rdr["UserType"].ToString();
                    user.UserPhonenum = rdr["UserPhoneNum"].ToString();
                    user.UserAddress = rdr["UserAddress"].ToString();
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            connection.Close();
            return Ok(user);
        }

    }
}