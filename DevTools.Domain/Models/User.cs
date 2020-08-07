using System;
using System.Collections.Generic;

namespace DevTools.Domain.Models
{
    public class User
    {
        public User()
        {
            UserTokens = new HashSet<UserToken>();
            UserApplications = new HashSet<UserApplication>();
            GroupTemplates = new HashSet<GroupTemplate>();
            Transactions = new HashSet<Transaction>();
            GroupTest = new HashSet<GroupTest>();
        }

        public Guid Id { get; set; }
        public int RoleId { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string ConfirmCode { get; set; }

        public bool IsEmailConfirm { get; set; }
        public bool IsMobileConfirm { get; set; }

        public double Wallet { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; }
        public virtual ICollection<UserApplication> UserApplications { get; }
        public virtual ICollection<GroupTemplate> GroupTemplates { get; }
        public virtual ICollection<Transaction> Transactions { get; }
        public virtual ICollection<GroupTest> GroupTest { get; }

    }
}