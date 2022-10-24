using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class UserToSponsor
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public uint SponsorId { get; set; }
        public double UserPoints { get; set; }
        public int? SponsorTotal { get; set; }
    }
}
