using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class Catalogue
    {
        public Catalogue()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int SponsorId { get; set; }

        public virtual SponsorOrg Sponsor { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
