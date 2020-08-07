using System;
using DevTools.Common.Enum;

namespace DevTools.Domain.Models
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Os { get; set; }
        public string Browser { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public string Token { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }

        public bool IsExpired { get; set; }
        public TokenType TokenType { get; set; }

        public virtual User User { get; set; }

    }
}