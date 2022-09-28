using System;
using System.ComponentModel.DataAnnotations;

namespace api_backend.Procedures
{
    public class UserLoginRequest
    {
        public string Username { get; set; } = null!;

        [Required, ]
        public string UserPwd { get; set; } = null!;
    }
}

