using System;
namespace api_backend.Models
{
    public class Users
    {
        public Guid UserId { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public Guid SponsorID { get; set; }

        public string? UserType { get; set; }

        public string? UserAddress { get; set; }

        public string? UserEmail { get; set; }

        public string? UserPhonenum { get; set; }

        public string? UserPronouns { get; set; }

        public string? UserPwd { get; set; }

    }
}

