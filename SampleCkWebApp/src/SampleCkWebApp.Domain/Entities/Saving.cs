using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Saving
    {
        public int Id { get; set; }
        public required string Name { get; set; }           // "Holiday to Paris"
        public string? Description { get; set; }
        public decimal TargetAmount { get; set; }  // Goal: $5000
        public decimal CurrentAmount { get; set; } // Saved so far: $3000
        public DateTime CreatedAt { get; set; }
        public DateTime? TargetDate { get; set; }
        public SavingStatus Status { get; set; }   // Active, Completed, etc.

        // Navigation properties
        public ICollection<UserSaving> UserSavings { get; set; } = new List<UserSaving>();
        public ICollection<Transaction> IncomeTransactions { get; set; } = new List<Transaction>();
    }
}