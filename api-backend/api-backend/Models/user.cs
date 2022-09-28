using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MazedDB.Models
{
    public partial class user
    {
        public int Id { get; set; }
        public int? SponsorId { get; set; }
        public string Username { get; set; } = null!;
        public string UserFname { get; set; } = null!;
        public string UserLname { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public string? UserAddress { get; set; }
        [Required, EmailAddress]
        public string? UserEmail { get; set; }
        public string? UserPhoneNum { get; set; }
        public int Blacklist { get; set; }
    }
}
