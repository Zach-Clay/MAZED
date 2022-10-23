using System;
using System.Collections.Generic;

namespace MazedDB.Models
{
    public partial class LoginAttempt
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public DateTime AttemptedDate { get; set; }
<<<<<<< HEAD
        public sbyte IsLoginSuccessful { get; set; }
=======
        public sbyte IsLoginSuccessful { get; set; } = 1;
>>>>>>> c45ad7af2a252f12167d82ed46d8dc853cd1e39b
    }
}
