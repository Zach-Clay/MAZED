using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int SponsorId { get; set; }
        public int? OrderId { get; set; }
        public int TrackId { get; set; }
        public int? ItemCost { get; set; }
    }
}
