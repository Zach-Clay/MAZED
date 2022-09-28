using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class Application
    {
        public int UserId { get; set; }
        public int SponsorId { get; set; }
        public sbyte ApprovalStatus { get; set; }
        public string? Description { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public sbyte IsActive { get; set; }
    }
}
