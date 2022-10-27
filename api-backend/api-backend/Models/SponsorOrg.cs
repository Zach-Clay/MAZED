using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class SponsorOrg
    {
        public SponsorOrg()
        {
            DriverOrders = new HashSet<DriverOrder>();
        }

        public int Id { get; set; }
        public string OrgName { get; set; } = null!;
        public string OrgDescription { get; set; } = null!;
        public int CatalogueId { get; set; }
        public double DollarToPoint { get; set; }
        public sbyte IsBlacklisted { get; set; }
        public double DailyPointAmount { get; set; }

        public virtual AuditLogging? AuditLogging { get; set; }
        public virtual ICollection<DriverOrder> DriverOrders { get; set; }
    }
}
