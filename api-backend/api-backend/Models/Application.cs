using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SponsorId { get; set; }
        public sbyte? ApprovalStatus { get; set; }
        public string? ApplicantName { get; set; }
        public string? SponsorName { get; set; }
        public string Description { get; set; } = null!;
        public DateOnly RequestedDate { get; set; }
        public DateOnly? ResponseDate { get; set; }
        public string? DecisionReason { get; set; }
        public sbyte IsActive { get; set; }
    }
}
