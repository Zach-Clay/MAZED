using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class SponsorOrg
    {
        public SponsorOrg()
        {
            Catalogues = new HashSet<Catalogue>();
            DriverOrders = new HashSet<DriverOrder>();
            PointTransactions = new HashSet<PointTransaction>();
        }

        public int Id { get; set; }
        public string OrgName { get; set; } = null!;
        public string OrgDescription { get; set; } = null!;
        public int CatalogueId { get; set; }
        public sbyte IsBlacklisted { get; set; }

        public virtual AuditLogging? AuditLogging { get; set; }
        public virtual ICollection<Catalogue> Catalogues { get; set; }
        public virtual ICollection<DriverOrder> DriverOrders { get; set; }
        public virtual ICollection<PointTransaction> PointTransactions { get; set; }
    }
}
