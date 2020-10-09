using System;
using DevTools.Application.Users.Model;
using DevTools.Common.Enum;
using DevTools.Domain.Models;

namespace DevTools.Application.Transactions.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public double Price { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        public string RefId { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public double Wallet { get; set; }
    }
}