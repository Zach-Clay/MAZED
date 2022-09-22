using System;
using Microsoft.Extensions.Configuration;
using api_backend.Models;
using MySqlConnector;
using api_backend;

namespace api_backend.Logic
{
    public class UserLogic
    {
        //Get user with id = id
        public static User getUser(string username)
        {
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

            var user = new User();
            try
            {
                conn.Open();

                string sql = "SELECT * FROM TEAM2_DB.users WHERE Username=@username";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //add parameters
                cmd.Parameters.AddWithValue("@username", username);


                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.Id = (int)rdr["Id"];
                    if (rdr["SponsorID"].ToString() != "") user.SponsorID = (int)rdr["SponsorID"];
                    user.Username = rdr["Username"].ToString();
                    user.FName = rdr["UserFName"].ToString();
                    user.LName = rdr["UserLName"].ToString();
                    user.Type = rdr["UserType"].ToString();
                    user.Address = rdr["UserAddress"].ToString();
                    user.Email = rdr["UserLName"].ToString();
                    user.Phonenum = rdr["UserPhoneNum"].ToString();
                    user.Pronouns = rdr["UserPronouns"].ToString();
                    user.Pwd = rdr["UserPwd"].ToString();
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return user;
        }//end getUser
    }
}
