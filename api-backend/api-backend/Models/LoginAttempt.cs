using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class LoginAttempt
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string AttemptedDate { get; set; } = null!;
        public string IsLoginSuccessful { get; set; } = null!;

        public virtual User IdNavigation { get; set; } = null!;
    }
}
