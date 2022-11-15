using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class DriverOrder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SponsorId { get; set; }
        public string OrderStatus { get; set; } = null!;
        public double TotalPointVal { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductList { get; set; } = null!;

        public virtual SponsorOrg Sponsor { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
