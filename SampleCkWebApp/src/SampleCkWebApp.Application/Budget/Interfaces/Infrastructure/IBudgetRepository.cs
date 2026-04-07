using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Budget.Interfaces.Infrastructure;

public interface IBudgetRepository : IRepository<Domain.Entities.Budget>
{
    Task<IEnumerable<Domain.Entities.Budget>> GetUserBudgetsAsync(int userId);
    Task<Domain.Entities.Budget?> GetBudgetForCategoryAndMonthAsync(int categoryId, int month, int year);

    Task<IEnumerable<Domain.Entities.Budget>> GetUserBudgetsForMonthAsync(int userId, int month, int year);

    Task<(decimal totalBudgetAmount, decimal totalSpentAmount, int budgetCount)> 
        GetUserBudgetSummaryAsync(int userId);

    Task<(decimal totalBudgetAmount, decimal totalSpentAmount, int budgetCount)> 
        GetUserBudgetSummaryForMonthAsync(int userId, int month, int year);

    Task<bool> BudgetExistsForMonthAsync(int categoryId, int month, int year);
    Task<IEnumerable<Domain.Entities.Budget>> GetBudgetsByCategoryIdAsync(int categoryId);
}