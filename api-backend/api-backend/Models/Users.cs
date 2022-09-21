using System;
using Microsoft.EntityFrameworkCore;

namespace api_backend.Models
{
    public class users
    {
        public Guid Id { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public string? Username { get; set; }

        public Guid SponsorID { get; set; }

        public string? UserType { get; set; }

        public string? UserAddress { get; set; }

        public string? UserEmail { get; set; }

        public string? UserPhonenum { get; set; }

        public string? UserPronouns { get; set; }

        public string? UserPwd { get; set; }

    }
}

