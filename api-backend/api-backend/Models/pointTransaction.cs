using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class PointTransaction
    {
        public int Id { get; set; }
        public int SponsorId { get; set; }
        public int UserId { get; set; }
        public int PointValue { get; set; }
        public string? Reason { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual SponsorOrg Sponsor { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
