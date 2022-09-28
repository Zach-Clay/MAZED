using System;
using System.ComponentModel.DataAnnotations;

namespace api_backend.Procedures
{
    public class UserLoginRequest
    {
        public string Username { get; set; } = null!;

<<<<<<< HEAD
        [Required, ]
=======
        [Required]
>>>>>>> Evan/Dev
        public string UserPwd { get; set; } = null!;
    }
}

