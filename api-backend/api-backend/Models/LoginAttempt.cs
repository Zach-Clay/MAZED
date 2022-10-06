using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class LoginAttempt
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public DateTime AttemptedDate { get; set; }
        public string IsLoginSuccessful { get; set; } = null!;

        public virtual User IdNavigation { get; set; } = null!;
    }
}
