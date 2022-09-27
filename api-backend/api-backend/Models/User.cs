using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int? SponsorId { get; set; }
        public string Username { get; set; } = null!;
        public string UserFname { get; set; } = null!;
        public string UserLname { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public string? UserAddress { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNum { get; set; }
        public int Blacklist { get; set; }
    }
}
