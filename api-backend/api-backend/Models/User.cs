using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class User
    {
        public User()
        {
            DriverOrders = new HashSet<DriverOrder>();
            PwdChanges = new HashSet<PwdChange>();
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
        public string? UserPwd { get; set; }
        public sbyte IsBlacklisted { get; set; }
        public sbyte? PointNotifications { get; set; }
        public sbyte? OrderNotifications { get; set; }
        public sbyte? IssueNotifications { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? ModDate { get; set; }
        public string? ModBy { get; set; }
        public double TotalPoints { get; set; }

        public virtual AuditLogging? AuditLogging { get; set; }
        public virtual ICollection<DriverOrder> DriverOrders { get; set; }
        public virtual ICollection<PwdChange> PwdChanges { get; set; }
    }
}
