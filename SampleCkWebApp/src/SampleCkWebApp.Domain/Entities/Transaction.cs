using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int? SavingId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }  // "income" or "expense"
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public User? User { get; set; }
        public Category? Category { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public Saving? Saving { get; set; }
    }
}