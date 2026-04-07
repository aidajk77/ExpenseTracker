using ErrorOr;
using SampleCkWebApp.Application.Category.Interfaces.Application;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Contracts.DTOs.Category;
using Domain.Entities;
using api.Mappers;
using Domain.Errors;
using SampleCkWebApp.Application.Budgets.Interfaces.Application;
using Contracts.DTOs.Budget;
using Domain.Enums;
using SampleCkWebApp.Application.Budget.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Category.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Transaction.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Category;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBudgetRepository _budgetRepository;
    private readonly CategoryValidator _categoryValidator;
    private readonly IBudgetService _budgetService;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IBudgetRepository budgetRepository,
        ITransactionRepository transactionRepository,
        CategoryValidator categoryValidator,
        IBudgetService budgetService)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
        _budgetService = budgetService;
    }

    public async Task<ErrorOr<IEnumerable<CategoryDto>>> GetAllUserCategoriesAsync(
        int userId, 
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetUserCategoriesAsync(userId);
        return categories.Select(c => c.ToDto()).ToList();
    }

    public async Task<ErrorOr<CategoryDto>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return CategoryErrors.NotFound;

        return category.ToDto();
    }

    public async Task<ErrorOr<CategoryDto>> CreateCategoryAsync(CreateCategoryDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input
        var validationResult = _categoryValidator.ValidateCreateCategory(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var category = request.ToModel();

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();


        return category.ToDto();
    }

    public async Task<ErrorOr<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input
        var validationResult = _categoryValidator.ValidateUpdateCategory(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return CategoryErrors.NotFound;

        //  Update only provided fields
        if (!string.IsNullOrEmpty(request.Name))
            category.Name = request.Name;
        if(request.AllTimeAmountSpent.HasValue)
            category.AllTimeAmountSpent = (decimal)request.AllTimeAmountSpent;
        if(request.AllTimeAmountEarned.HasValue)
            category.AllTimeAmountEarned = (decimal)request.AllTimeAmountEarned;
        

        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return category.ToDto();
    }

    public async Task<ErrorOr<CategoryDto>> UpdateCategoryAsync(
        int id,
        TransactionType type,
        decimal amountChange,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return CategoryErrors.NotFound;

        //  Validate amount
        if (amountChange == 0)
            return Error.Validation("Category.ZeroAmount", "Amount change cannot be zero");

        //  Handle based on transaction type
        if (type == TransactionType.INCOME)
        {
            //  INCOME updates AllTimeEarned
            var newEarned = category.AllTimeAmountEarned + amountChange;
            if (newEarned < 0)
                return Error.Validation("Category.InvalidAmount", "Earned amount cannot be negative");

            category.AllTimeAmountEarned = newEarned;
            Console.WriteLine($"[CATEGORY] Updated AllTimeEarned for category {id}: {amountChange} (Total: {newEarned})");
        }
        else if (type == TransactionType.EXPENSE)
        {
            //  EXPENSE updates AllTimeAmountSpent and Budget
            var newSpent = category.AllTimeAmountSpent + amountChange;
            if (newSpent < 0)
                return Error.Validation("Category.InvalidAmount", "Spent amount cannot be negative");

            category.AllTimeAmountSpent = newSpent;
            Console.WriteLine($"[CATEGORY] Updated AllTimeAmountSpent for category {id}: {amountChange} (Total: {newSpent})");

            //  Update budget for EXPENSE type
            var now = DateTime.UtcNow;
            var budget = await _budgetRepository.GetBudgetForCategoryAndMonthAsync(
                id,
                now.Month,
                now.Year);
            
            if (budget != null)
            {
                var budgetResult = await _budgetService.UpdateCurrentAmountAsync(
                    budget.Id,
                    amountChange,
                    cancellationToken);

                if (budgetResult.IsError)
                    Console.WriteLine($"[ERROR] Failed to update budget: {budgetResult.Errors.First().Description}");

                Console.WriteLine($"[BUDGET] Updated budget {budget.Id}: {amountChange}");
            }
            else
            {
                Console.WriteLine($"[BUDGET] ⚠️ No budget found for category {id}, month {now.Month}, year {now.Year}");
            }
        }

        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return category.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return CategoryErrors.NotFound;

        try
        {
            //  Get all budgets for this category
            var budgets = await _budgetRepository.GetBudgetsByCategoryIdAsync(id);

            //  Delete each budget
            foreach (var budget in budgets)
            {
                await _budgetRepository.DeleteAsync(budget);
            }
            await _budgetRepository.SaveChangesAsync();

            // Get all transactions for this category
            var transactions = await _transactionRepository.GetByCategoryIdAsync(id);

            // Delete each transaction
            foreach (var transaction in transactions)
            {
                await _transactionRepository.DeleteAsync(transaction);
            }
            await _transactionRepository.SaveChangesAsync();

            // Delete the category
            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Failure("Category.DeleteFailed", $"Failed to delete category: {ex.Message}");
        }
    }
}