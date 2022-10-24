using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class UserToSponsor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SponsorId { get; set; }
        public double UserPoints { get; set; }
        public string UserType { get; set; } = null!;
    }
}
