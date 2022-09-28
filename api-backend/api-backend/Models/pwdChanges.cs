using System;
namespace api_backend.Models
{
    public class pwdChanges
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ChangedPwd { get; set; }
        public string? OgPWd { get; set; }
        
    }
}

