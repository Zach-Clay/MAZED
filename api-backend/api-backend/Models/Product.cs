using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int SponsorId { get; set; }
        public int OrderId { get; set; }
        public int PointValue { get; set; }
        public int OrderQuantity { get; set; }
        public sbyte Availibility { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public string Name { get; set; } = null!;

        public virtual SponsorOrg Sponsor { get; set; } = null!;
    }
}
