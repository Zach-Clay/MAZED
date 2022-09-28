using System;
namespace api_backend.Models
{
    public class auditLogging
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SponsorId { get; set; }
        public string? Type { get; set; }
        public string? description { get; set; }
        public string? AppStatus { get; set; }
        public DateTime Date { get; set; }
        public string? AppREason { get; set; }
        public int PointChange { get; set; }
        public string? PointReason { get; set; }
        public string? PasswordReason { get; set; }
        public string? LoginSF { get; set; }
    }
}

