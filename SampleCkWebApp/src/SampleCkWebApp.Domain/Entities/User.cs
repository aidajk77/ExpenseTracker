using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public  string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FK
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        // Navigation
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<UserSaving> UserSavings { get; set; } = new List<UserSaving>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}