using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Budget
{
    public class UpdateBudgetDto
    {
        public decimal? AmountLimit { get; set; }
        public decimal? CurrentAmount { get; set; }
    }
}