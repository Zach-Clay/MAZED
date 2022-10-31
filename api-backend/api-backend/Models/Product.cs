using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int SponsorId { get; set; }
        public int? OrderId { get; set; }
        public string TrackId { get; set; } = null!;
    }
}
