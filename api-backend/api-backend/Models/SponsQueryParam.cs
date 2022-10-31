using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class SponsQueryParam
    {
        public static string iTunes_url = "https://itunes.apple.com";
        public int Id { get; set; }
        public string? MediaType { get; set; }
        public string? Entities { get; set; }
        public string? Attributes { get; set; }
        public int SponsorId { get; set; }
        public int? Limit { get; set; }
    }
}
