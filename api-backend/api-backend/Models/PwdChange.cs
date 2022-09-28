using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class PwdChange
    {
        public int Id { get; set; }
        public string? ChangedPwd { get; set; }
        public string? OgPwd { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
