using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCkWebApp.Contracts.DTOs.Budget
{
    public class BudgetSummaryDto
    {
        public int UserId { get; set; }
        public decimal TotalBudgetAmount { get; set; }  // Sum of all AmountLimit
        public decimal TotalSpentAmount { get; set; }   // Sum of all CurrentAmount
        public decimal TotalRemainingAmount { get; set; }  // TotalBudgetAmount - TotalSpentAmount
        public decimal SpentPercentage { get; set; }  // (TotalSpent / TotalBudget) * 100
        public int BudgetCount { get; set; }  // Number of budgets
        public int Month { get; set; }  // If monthly
        public int Year { get; set; }   // If monthly
    }
}