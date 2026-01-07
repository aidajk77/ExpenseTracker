using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserSaving
    {
        public int UserId { get; set; }
        public int SavingId { get; set; }
        public decimal ContributedAmount { get; set; } 
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User? User { get; set; }
        public Saving? Saving { get; set; }
    }
}