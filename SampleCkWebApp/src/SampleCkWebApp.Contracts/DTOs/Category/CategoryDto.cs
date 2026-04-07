using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal AllTimeAmountSpent { get; set; }
        public decimal AllTimeAmountEarned { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}