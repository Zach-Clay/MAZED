using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class AuditLogging
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? UserId { get; set; }
        public string? SponsorId { get; set; }
        public string? AppStatus { get; set; }
        public DateTime? Date { get; set; }
        public string? AppReason { get; set; }
        public int? PointChange { get; set; }
        public string? PointReason { get; set; }
        public string? PasswordReason { get; set; }
        public string? LoginSf { get; set; }

        public virtual User Id1 { get; set; } = null!;
        public virtual SponsorOrg IdNavigation { get; set; } = null!;
    }
}
