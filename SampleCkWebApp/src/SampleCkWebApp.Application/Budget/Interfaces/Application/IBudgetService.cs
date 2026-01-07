using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Budget;
using ErrorOr;

namespace SampleCkWebApp.Application.Budgets.Interfaces.Application
{
    public interface IBudgetService
    {
        Task<ErrorOr<IEnumerable<BudgetDto>>> GetAllBudgetsAsync(CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetDto>> GetBudgetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetDto>> CreateBudgetAsync(CreateBudgetDto request, CancellationToken cancellationToken = default);
        Task<ErrorOr<BudgetDto>> UpdateBudgetAsync(int id, UpdateBudgetDto request, CancellationToken cancellationToken = default);
        Task<ErrorOr<Success>> DeleteBudgetAsync(int id, CancellationToken cancellationToken = default);
    }
}