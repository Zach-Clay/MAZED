using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class User
    {
        public User()
        {
            DriverOrders = new HashSet<DriverOrder>();
            PointTransactions = new HashSet<PointTransaction>();
        }

        public int Id { get; set; }
        public int? SponsorId { get; set; }
        public string Username { get; set; } = null!;
        public string UserFname { get; set; } = null!;
        public string UserLname { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public string? UserAddress { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNum { get; set; }
        //public string? UserPronouns { get; set; }
        public string UserPwd { get; set; } = null!;
        public bool blacklist { get; set; } = false;

        public virtual SponsorOrg? Sponsor { get; set; }
        public virtual ICollection<DriverOrder>? DriverOrders { get; set; }
        public virtual ICollection<PointTransaction>? PointTransactions { get; set; }
    }
}
