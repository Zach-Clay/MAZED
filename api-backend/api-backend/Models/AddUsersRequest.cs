//want to get certain info from DB

using System;
using System.ComponentModel.DataAnnotations;

namespace api_backend.Models
{
    public class AddUsersRequest
    {
        //removed ID b/c we give the ID

        public string? UserAddress { get; set; }

        [Required, EmailAddress]
        public string? UserEmail { get; set; }

        public string? UserPhonenum { get; set; }

        public string? UserPronouns { get; set; }

        public string Username { get; set; } = null!;

        public string UserFname { get; set; } = null!;

        public string UserLname { get; set; } = null!;

        public string UserType { get; set; } = null!;

        [Required, MinLength(8, ErrorMessage = "Please enter a passowrd of at least 8 characters in length.")]
        public string UserPwd { get; set; } = null!;

        //a string for confirming the password when registering
        //[Required, Compare("UserPwd")]
        //public string ConfirmPwd { get; set; } = null!;
    }
}

