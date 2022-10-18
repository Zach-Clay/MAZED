using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class PointTransaction
    {
        public int PointId { get; set; }
        public int SponsorId { get; set; }
        public int UserId { get; set; }
        public double PointValue { get; set; }
        public string? Reason { get; set; }
        public DateTime ModDate { get; set; }
        public sbyte isSpecialTransaction { get; set; } = 0;
    }
}
