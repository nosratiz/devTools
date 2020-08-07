using System;

namespace DevTools.Domain.Models
{
    public class UserApplication
    {
        public Guid UserId { get; set; }

        public string SecretCode { get; set; }
        public string RestrictIp { get; set; }
        public int UsageCount { get; set; }

        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
    }
}