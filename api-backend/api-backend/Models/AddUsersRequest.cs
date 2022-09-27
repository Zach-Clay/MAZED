﻿//want to get certain info from DB

using System;
namespace api_backend.Models
{
    public class AddUsersRequest
    {
        //removed ID b/c we give the ID

        public string? FName { get; set; }

        public string? LName { get; set; }

        public string? Username { get; set; }

        public string? UserType { get; set; }

        public string? UserAddress { get; set; }

        public string? UserEmail { get; set; }

        public string? UserPhonenum { get; set; }

        public string? UserPronouns { get; set; }

        public string? UserPwd { get; set; }
    }
}
