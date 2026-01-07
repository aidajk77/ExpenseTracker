using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Contracts.DTOs.Transaction
{
    public class CreateTransactionDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int PaymentMethodId { get; set; }
        public int? SavingId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }  // Income or Expense
        public string? Description { get; set; }
        public DateTime Date { get; set; }

    }
}