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
        //how we talk to db
        //private readonly UsersAPIDBContext dbcontext;


        //public UsersController(UsersAPIDBContext dbcontext)
        //{
        //    this.dbcontext = dbcontext;
        //}

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

        //[HttpGet]
        //[Route("{id:guid}")]
        ////talk to database async
        //public async Task<IActionResult> GetUser([FromRoute] Guid id)
        //{
        //    var user = await dbContext.Users.FindAsync(id);
        //    if(user == null)
        //    {
        //        return NotFound();
        //    }
        //    //inject DB context, wrap for response
        //    return Ok(user);
        //}

        //[HttpPost]
        //public IActionResult AddUsers(AddUsersRequest addUsers)
        //{
        //    var user = new users()
        //    {
        //        Id = Guid.NewGuid(),
        //        SponsorID = Guid.NewGuid(),
        //        FName = addUsers.FName,
        //        LName = addUsers.LName,
        //        Username = addUsers.Username,
        //        UserType = addUsers.UserType,
        //        UserAddress = addUsers.UserAddress,
        //        UserEmail = addUsers.UserEmail,
        //        UserPhonenum = addUsers.UserPhonenum,
        //        UserPronouns = addUsers.UserPronouns,
        //        UserPwd = addUsers.UserPwd
               
        //    };
        //    await dbContext.Users.AddAsync(user);
        //    await dbContext.SaveChangesAsync();

        //    return Ok(user);
        //}

        //[HttpPut]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> UpdateUsers([FromRoute] Guid id, UpdateUsersRequest updateUsersRequest)
        //{
        //    var user = await dbContext.Users.FindAsync(id);

        //    if (user != null)
        //    {
        //        user.SponsorID = Guid.NewGuid();
        //        user.FName = updateUsersRequest.FName;
        //        user.LName = updateUsersRequest.LName;
        //        user.Username = updateUsersRequest.Username;
        //        user.UserType = updateUsersRequest.UserType;
        //        user.UserAddress = updateUsersRequest.UserAddress;
        //        user.UserEmail = updateUsersRequest.UserEmail;
        //        user.UserPhonenum = updateUsersRequest.UserPhonenum;
        //        user.UserPronouns = updateUsersRequest.UserPronouns;
        //        user.UserPwd = updateUsersRequest.UserPwd;

        //        await dbContext.SaveChangesAsync();
        //        return Ok(user);
        //    }

        //    return NotFound();
            
        //}

        //[HttpDelete]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> DeleteUsers([FromRoute] Guid id)
        //{
        //    var user = await dbContext.Users.FindAsync(id);

        //    if (user != null)
        //    {
        //        dbContext.Remove(user);

        //        await dbContext.SaveChangesAsync();
        //        return Ok(user);
        //    }

        //    return NotFound();

        //}


    }
}