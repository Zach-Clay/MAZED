using System;
using Microsoft.EntityFrameworkCore;

namespace api_backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public string? Username { get; set; }

        public int SponsorID { get; set; }

        public string? Type { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phonenum { get; set; }

        public string? Pronouns { get; set; }

        public string? Pwd { get; set; }

    }
}

