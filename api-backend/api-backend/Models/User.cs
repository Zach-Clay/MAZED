using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class User
    {
        public User()
        {
            DriverOrders = new HashSet<DriverOrder>();
            PointTransations = new HashSet<PointTransation>();
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

        public virtual AuditLogging? AuditLogging { get; set; }
        public virtual ICollection<DriverOrder> DriverOrders { get; set; }
        public virtual ICollection<PointTransation> PointTransations { get; set; }
        public virtual ICollection<PwdChange> PwdChanges { get; set; }
    }
}
