using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class sponsorOrg
    {
        public sponsorOrg()
        {
            Catalogues = new HashSet<catalogue>();
            DriverOrders = new HashSet<driverOrders>();
            PointTransactions = new HashSet<pointTransaction>();
            Users = new HashSet<user>();
        }

        public int Id { get; set; }
        public string OrgName { get; set; } = null!;
        public string OrgDescription { get; set; } = null!;
        public int CatalogueId { get; set; }
        public Boolean Blacklist { get; set; }

        public virtual ICollection<catalogue> Catalogues { get; set; }
        public virtual ICollection<driverOrders> DriverOrders { get; set; }
        public virtual ICollection<pointTransaction> PointTransactions { get; set; }
        public virtual ICollection<user> Users { get; set; }
    }
}
