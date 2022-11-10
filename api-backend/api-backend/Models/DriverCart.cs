using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class DriverCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SponsorId { get; set; }
        public int? MediaType { get; set; }
        public int PointValue { get; set; }
        public int? ProductId { get; set; }
    }
}
