using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Budget;
using ErrorOr;
using SampleCkWebApp.Contracts.DTOs.Budget;

namespace SampleCkWebApp.Application.Budgets.Interfaces.Application
{
    public interface IBudgetService
    {
        Task<ErrorOr<IEnumerable<BudgetDto>>> GetAllBudgetsAsync(CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetDto>> GetBudgetByIdAsync(int id, int userId, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<BudgetDto>>> GetUserBudgetsForMonthAsync(int userId, int month, int year, CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetSummaryDto>> GetUserBudgetSummaryAsync(int userId, CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetSummaryDto>> GetUserBudgetSummaryForMonthAsync(int userId, int month, int year, CancellationToken cancellationToken = default);
        Task<ErrorOr<IEnumerable<BudgetDto>>> GetUserBudgetsAsync(int userId, CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetDto>> CreateBudgetAsync(CreateBudgetDto request, int userId, CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetDto>> UpdateBudgetAsync(int id, int userId, UpdateBudgetDto request, CancellationToken cancellationToken = default);
        Task<ErrorOr<Success>> UpdateCurrentAmountAsync(int budgetId, decimal amountChange, CancellationToken cancellationToken = default);
        Task<ErrorOr<Success>> DeleteBudgetAsync(int id, CancellationToken cancellationToken = default);
    }
}