using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class pointTransaction
    {
        public int pointId { get; set; }
        public int sponsorId { get; set; }
        public int userId { get; set; }
        public int pointValue { get; set; }
        public string? Reason { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual sponsorOrg Sponsor { get; set; } = null!;
        public virtual user User { get; set; } = null!;
    }
}
