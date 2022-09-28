using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class application
    {
        public int userId { get; set; }
        public int sponsorId { get; set; }
        public sbyte approvalStatus { get; set; }
        public string? description { get; set; }
        public DateTime requestedDate { get; set; }
        public DateTime? responseDate { get; set; }
        public sbyte isActive { get; set; }
    }
}
