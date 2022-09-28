using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class catalogue
    {
        public catalogue()
        {
            Products = new HashSet<product>();
        }

        public int Id { get; set; }
        public int SponsorId { get; set; }

        public virtual sponsorOrg Sponsor { get; set; } = null!;
        public virtual ICollection<product> Products { get; set; }
    }
}
