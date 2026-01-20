using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using Contracts.DTOs.Budget;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using SampleCkWebApp.Application.Budget;
using SampleCkWebApp.Application.Budgets.Interfaces.Application;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Budgets
{
    public class BudgetService : IBudgetService
    {

        private readonly IRepository<Domain.Entities.Budget> _budgetRepository;
        private readonly IRepository<Domain.Entities.Category> _categoryRepository;
        private readonly BudgetValidator _budgetValidator;

        public BudgetService(
            IRepository<Domain.Entities.Budget> budgetRepository,
            IRepository<Domain.Entities.Category> categoryRepository,
            BudgetValidator budgetValidator)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _budgetValidator = budgetValidator;
        }
        public async Task<ErrorOr<BudgetDto>> CreateBudgetAsync(CreateBudgetDto request, CancellationToken cancellationToken = default)
        {

            //  Validate input using validator
            var validationResult = _budgetValidator.ValidateCreateBudget(request);
            if (validationResult.IsError)
                return validationResult.Errors;
                
            // Validate CategoryId exists
            var categoryExists = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (categoryExists == null)
                return BudgetErrors.CategoryNotFound;

            //  Check for duplicate budget
            var existingBudgets = await _budgetRepository.GetAllAsync();
            if (existingBudgets.Any(b => b.CategoryId == request.CategoryId
                && b.Month == request.Month
                && b.Year == request.Year))
                return BudgetErrors.DuplicateBudget;  //  Use domain error

            var budget = request.ToModel();

            await _budgetRepository.AddAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            return budget.ToDto();
        }

        public async Task<ErrorOr<Success>> DeleteBudgetAsync(int id, CancellationToken cancellationToken = default)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget == null)
                return BudgetErrors.NotFound;

            await _budgetRepository.DeleteAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<ErrorOr<IEnumerable<BudgetDto>>> GetAllBudgetsAsync(CancellationToken cancellationToken = default)
        {
            var budgets = await _budgetRepository.GetAllAsync();
            return budgets.Select(b => b.ToDto()).ToList();
        }

        public async Task<ErrorOr<BudgetDto>> GetBudgetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget == null)
                return BudgetErrors.NotFound;

            return budget.ToDto();
        }

        public async Task<ErrorOr<BudgetDto>> UpdateBudgetAsync(int id, UpdateBudgetDto request, CancellationToken cancellationToken = default)
        {
            //  Validate input using validator
            var validationResult = _budgetValidator.ValidateUpdateBudget(request);
            if (validationResult.IsError)
                return validationResult.Errors;

            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget == null)
                return BudgetErrors.NotFound;

            // Update only provided fields
            if (request.AmountLimit.HasValue)
                budget.AmountLimit = request.AmountLimit.Value;

            await _budgetRepository.UpdateAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            return budget.ToDto();
        }
    }
}