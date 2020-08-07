using System;
using DevTools.Common.Enum;

namespace DevTools.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public double Price { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        public string RefId { get; set; }

        public DateTime CreateDate { get; set; }
       
        public virtual User User { get; set; }
    }
}