using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Budget
{
    public class CreateBudgetDto
    {
        public int CategoryId { get; set; }
        public decimal AmountLimit { get; set; }
        public int Month { get; set; } 
        public int Year { get; set; }
    }
}