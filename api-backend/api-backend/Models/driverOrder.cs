using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class driverOrders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SponsorId { get; set; }
        public string OrderStatus { get; set; } = null!;
        public int TotalPointVal { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual sponsorOrg Sponsor { get; set; } = null!;
        public virtual user User { get; set; } = null!;
    }
}
