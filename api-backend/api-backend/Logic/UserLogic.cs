using System;
using api_backend.Models;
using MySqlConnector;
using api_backend;
using MazedDB.Models;
using Org.BouncyCastle.Ocsp;

namespace api_backend.Logic
{
    public class UserLogic
    {
<<<<<<< HEAD
        //Get all the users
        public static List<user> getAllUsers()
        {
            //Get the conn info
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

            //create a user to return
            List<user> users = new List<user>();
            try
            {
                //Open the connection
                conn.Open();

                //Create sql command
                string sql = @"SELECT * FROM TEAM2_DB.users";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //Execute the query and read the data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user user = new user();
                    user.Id = (int)rdr["Id"];
                    if (rdr["SponsorID"].ToString() != "") user.SponsorId = (int)rdr["SponsorID"];
                    user.Username = rdr["Username"].ToString();
                    user.UserFname = rdr["UserFName"].ToString();
                    user.UserLname = rdr["UserLName"].ToString();
                    user.UserType = rdr["UserType"].ToString();
                    user.UserAddress = rdr["UserAddress"].ToString();
                    user.UserEmail = rdr["UserEmail"].ToString();
                    user.UserPhoneNum = rdr["UserPhoneNum"].ToString();
                    user.Blacklist = (SByte)rdr["blacklist"];

                    //add each user to the list
                    if (user.Blacklist == 0) users.Add(user);
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
            return users;
        }//end getAllUsers

        //Get user with their id
        public static user getUser(int Id)
=======
        //Get user with their id
        public static User getUser(int Id)
>>>>>>> Evan/Dev
        {
            //Get the conn info
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

            //create a user to return
<<<<<<< HEAD
            var user = new user();
=======
            var user = new User();
>>>>>>> Evan/Dev
            try
            {
                //Open the connection
                conn.Open();

                //Create sql command
                string sql = @"SELECT * FROM TEAM2_DB.users WHERE Id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //add parameters (this removes the possibility of SQL injection)
                cmd.Parameters.AddWithValue("@Id", Id);

                //Execute the query and read the data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.Id = (int)rdr["Id"];
                    if (rdr["SponsorID"].ToString() != "") user.SponsorId = (int)rdr["SponsorID"];
                    user.Username = rdr["Username"].ToString();
                    user.UserFname = rdr["UserFName"].ToString();
                    user.UserLname = rdr["UserLName"].ToString();
                    user.UserType = rdr["UserType"].ToString();
                    user.UserAddress = rdr["UserAddress"].ToString();
<<<<<<< HEAD
                    user.UserEmail = rdr["UserEmail"].ToString();
                    user.UserPhoneNum = rdr["UserPhoneNum"].ToString();
                    user.Blacklist = (SByte)rdr["blacklist"];
=======
                    user.UserEmail = rdr["UserLName"].ToString();
                    user.UserPhoneNum = rdr["UserPhoneNum"].ToString();
                    //user.UserPronouns = rdr["UserPronouns"].ToString();
                    //user.UserPwd = rdr["UserPwd"].ToString();
>>>>>>> Evan/Dev
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

        //Add user to the db
<<<<<<< HEAD
        public static int registerUser(user user)
        {
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

=======
        public static int addUser(User user)
        {

            //WE WILL WANT TO MODIFY THIS TO BE REGISTER AS A USER
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);


            //CreatePwdHash(request.Pwd, out byte[] pwdHash, out byte[] pwdSalt);

            //random token
            //Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

>>>>>>> Evan/Dev
            int ret = 0;
            try
            {
                conn.Open();

<<<<<<< HEAD
                string sql = @"INSERT INTO TEAM2_DB.users VALUES (
                    @Id, @SponsorId, @Username, @FName, @LName, @Type, @Address,
                    @Email, @Phonenum, @Blacklist)";
=======
                string sql = "INSERT INTO TEAM2_DB.users VALUES (" +
                    "@Id, @SponsorId, @Username, @FName, @LName, @Type, @Address, " +
                    "@Email, @Phonenum, @Pronouns, @Pwd)";
>>>>>>> Evan/Dev
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                if (user.SponsorId == 0) cmd.Parameters.AddWithValue("@SponsorId", null);
                else cmd.Parameters.AddWithValue("@SponsorId", user.SponsorId);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@FName", user.UserFname);
                cmd.Parameters.AddWithValue("@LName", user.UserLname);
                cmd.Parameters.AddWithValue("@Type", user.UserType);
                cmd.Parameters.AddWithValue("@Address", user.UserAddress);
                cmd.Parameters.AddWithValue("@Email", user.UserEmail);
                cmd.Parameters.AddWithValue("@Phonenum", user.UserPhoneNum);
<<<<<<< HEAD
                cmd.Parameters.AddWithValue("@Blacklist", 0);
=======
                //cmd.Parameters.AddWithValue("@Pronouns", user.UserPronouns);
                //cmd.Parameters.AddWithValue("@Pwd", user.UserPwd);
>>>>>>> Evan/Dev

                //Execute the command
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

            return ret;
        }//end addUser


<<<<<<< HEAD
        //Change user with their id
        public static int changeUserWID(int Id, user user)
=======
        ////function to hash for security
        //public void CreatePwdHash(string pwd, out byte[] pwdHash, out byte[] pwdSalt)
        //{
        //    using(var hmac = new HMACSHA512())
        //    {
        //        pwdSalt = hmac.Key;
        //        pwdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwd));
        //    }
        //}


        //Change user with their id
        public static int changeUser(int Id, User user)
>>>>>>> Evan/Dev
        {
            //Get the conn info
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

            int ret = 0;

            try
            {
                //Open the connection
                conn.Open();

                //Create sql command
                string sql = "UPDATE TEAM2_DB.users SET" +
                    "(@Id, @SponsorId, @Username, @FName, @LName, @Type, " +
<<<<<<< HEAD
                    "@Address, @Email, @Phonenum, @Blacklist) WHERE " +
=======
                    "@Address, @Email, @Phonenum, @Pronouns, @Pwd) WHERE " +
>>>>>>> Evan/Dev
                    "Id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", user.Id);
                if (user.SponsorId == 0) cmd.Parameters.AddWithValue("@SponsorId", null);
                else cmd.Parameters.AddWithValue("@SponsorId", user.SponsorId);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@FName", user.UserFname);
                cmd.Parameters.AddWithValue("@LName", user.UserLname);
                cmd.Parameters.AddWithValue("@Type", user.UserType);
                cmd.Parameters.AddWithValue("@Address", user.UserAddress);
                cmd.Parameters.AddWithValue("@Email", user.UserEmail);
                cmd.Parameters.AddWithValue("@Phonenum", user.UserPhoneNum);
<<<<<<< HEAD
                cmd.Parameters.AddWithValue("@Blacklist", user.Blacklist);
=======
                //cmd.Parameters.AddWithValue("@Pronouns", user.UserPronouns);
                //cmd.Parameters.AddWithValue("@Pwd", user.UserPwd);
>>>>>>> Evan/Dev

                //Execute the command
                ret = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return ret;
        }//CHANGE USER END

        //Change user with their username
        public static int changeUserWUsername(string Username, user user)
        {
            //Get the conn info
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

            int ret = 0;

            try
            {
                //Open the connection
                conn.Open();

                //Create sql command
                string sql = "UPDATE TEAM2_DB.users SET" +
                    "(@Id, @SponsorId, @Username, @FName, @LName, @Type, " +
                    "@Address, @Email, @Phonenum, @Blacklist) WHERE " +
                    "Username = @Username";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", user.Id);
                if (user.SponsorId == 0) cmd.Parameters.AddWithValue("@SponsorId", null);
                else cmd.Parameters.AddWithValue("@SponsorId", user.SponsorId);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@FName", user.UserFname);
                cmd.Parameters.AddWithValue("@LName", user.UserLname);
                cmd.Parameters.AddWithValue("@Type", user.UserType);
                cmd.Parameters.AddWithValue("@Address", user.UserAddress);
                cmd.Parameters.AddWithValue("@Email", user.UserEmail);
                cmd.Parameters.AddWithValue("@Phonenum", user.UserPhoneNum);
                cmd.Parameters.AddWithValue("@Blacklist", user.Blacklist);

                //Execute the command
                ret = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return ret;
        }//CHANGE USER with username END

        //Change user with their id
        public static int deleteUser(int Id)
        {
            //Get the conn info
            string connStr = DBContext.ConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);

            int ret = 0;

            try
            {
                //Open the connection
                conn.Open();

                //Create sql command
                string sql = "DELETE FROM TEAM2_DB.users WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                //Execute the command
                ret = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return ret;
        }//DELETE USER END

    }
}
