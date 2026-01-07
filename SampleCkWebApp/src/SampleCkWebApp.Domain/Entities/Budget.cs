using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public decimal AmountLimit { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Category? Category { get; set; }
    }
}