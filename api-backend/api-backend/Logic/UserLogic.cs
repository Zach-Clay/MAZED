using System;
using api_backend.Models;
using MySqlConnector;
using api_backend;

namespace api_backend.Logic
{
    public class UserLogic
    {
        //Get user with username = username
        public static User getUser(string username)
        {
            //Get the conn info
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

            //create a user to return
            var user = new User();
            try
            {
                //Open the connection
                conn.Open();

                //Create sql command
                string sql = "SELECT * FROM TEAM2_DB.users WHERE Username=@username";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //add parameters (this removes the possibility of SQL injection)
                cmd.Parameters.AddWithValue("@username", username);

                //Execute the query and read the data
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

                //close the reader
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //close the conection
            conn.Close();

            //return the user
            return user;
        }//end getUser
    }
}
