using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Transaction.Interfaces.Application;
using SampleCkWebApp.Application.Category.Interfaces.Application;
using SampleCkWebApp.Application.Budgets.Interfaces.Application;
using Contracts.DTOs.Transaction;
using Domain.Entities;
using api.Mappers;
using Domain.Enums;
using Domain.Errors;
using SampleCkWebApp.Contracts.DTOs.Common;
using SampleCkWebApp.Application.Savings.Interfaces.Application;

namespace SampleCkWebApp.Application.Transaction;

public class TransactionService : ITransactionService
{
    private readonly IRepository<Domain.Entities.Transaction> _transactionRepository;
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;
    private readonly IRepository<Domain.Entities.Budget> _budgetRepository;
    private readonly IRepository<Domain.Entities.PaymentMethod> _paymentMethodRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Saving> _savingRepository;
    private readonly ICategoryService _categoryService;  
    private readonly IBudgetService _budgetService;   
    private readonly ISavingService _savingService;   
    private readonly TransactionValidator _transactionValidator;

    public TransactionService(
        IRepository<Domain.Entities.Transaction> transactionRepository,
        IRepository<Domain.Entities.Category> categoryRepository,
        IRepository<Domain.Entities.Budget> budgetRepository,
        IRepository<Domain.Entities.PaymentMethod> paymentMethodRepository,
        IRepository<User> userRepository,
        IRepository<Saving> savingRepository,
        ICategoryService categoryService,  
        IBudgetService budgetService,   
        ISavingService savingService,   
        TransactionValidator transactionValidator)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _budgetRepository = budgetRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _userRepository = userRepository;
        _savingRepository = savingRepository;
        _categoryService = categoryService; 
        _budgetService = budgetService;    
        _savingService = savingService;  
        _transactionValidator = transactionValidator;
    }

    public async Task<ErrorOr<PaginatedResponse<TransactionDto>>> GetPaginatedTransactionsAsync(
        int page = 1,
        int limit = 10,
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
            page = 1;

        if (limit < 1)
            limit = 10;

        if (limit > 100)
            limit = 100;

        var allTransactions = await _transactionRepository.GetAllAsync();
        var totalCount = allTransactions.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)limit);

        var transactions = allTransactions
            .OrderByDescending(t => t.Date)
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(t => t.ToDto())
            .ToList();

        var response = new PaginatedResponse<TransactionDto>
        {
            Data = transactions,
            Total = totalCount,
            Page = page,
            Limit = limit,
            TotalPages = totalPages
        };

        return response;
    }

    public async Task<ErrorOr<PaginatedResponse<TransactionDto>>> GetUserTransactionsPaginatedAsync(
        int userId,
        int page = 1,
        int limit = 10,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return TransactionErrors.InvalidUser;

        if (page < 1)
            page = 1;

        if (limit < 1)
            limit = 10;

        if (limit > 100)
            limit = 100;
        
        var allTransactions = await _transactionRepository.GetAllAsync();
        var userTransactions = allTransactions.Where(t => t.UserId == userId).ToList();

        var totalCount = userTransactions.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)limit);

        var transactions = userTransactions
            .OrderByDescending(t => t.Date)
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(t => t.ToDto())
            .ToList();

        var response = new PaginatedResponse<TransactionDto>
        {
            Data = transactions,
            Total = totalCount,
            Page = page,
            Limit = limit,
            TotalPages = totalPages
        };

        return response;
    }

    public async Task<ErrorOr<IEnumerable<TransactionDto>>> GetAllUserTransactionsAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return TransactionErrors.InvalidUser;

        var allTransactions = await _transactionRepository.GetAllAsync();
        
        var userTransactions = allTransactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .Select(t => t.ToDto())
            .ToList();

        return userTransactions;
    }

    public async Task<ErrorOr<decimal>> GetUserMonthlyIncomeAsync(
    int userId,
    int month,
    int year,
    CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return TransactionErrors.InvalidUser;

        var allTransactions = await _transactionRepository.GetAllAsync();
        
        var userIncome = allTransactions
            .Where(t => 
                t.UserId == userId && 
                t.Type == TransactionType.INCOME &&
                t.Date.Month == month &&
                t.Date.Year == year)
            .Sum(t => t.Amount);

        return userIncome;
    }

    public async Task<ErrorOr<decimal>> GetUserMonthlyExpenseAsync(
    int userId,
    int month,
    int year,
    CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return TransactionErrors.InvalidUser;

        var allTransactions = await _transactionRepository.GetAllAsync();
        
        var userIncome = allTransactions
            .Where(t => 
                t.UserId == userId && 
                t.Type == TransactionType.EXPENSE &&
                t.Date.Month == month &&
                t.Date.Year == year)
            .Sum(t => t.Amount);

        return userIncome;
    }
    public async Task<ErrorOr<TransactionDto>> GetTransactionByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return TransactionErrors.NotFound;

        return transaction.ToDto();
    }

    public async Task<ErrorOr<TransactionDto>> CreateTransactionAsync(
    CreateTransactionDto request,
    CancellationToken cancellationToken = default)
{   
    var validationResult = _transactionValidator.ValidateCreateTransaction(request);
    if (validationResult.IsError)
        return validationResult.Errors;

    var user = await _userRepository.GetByIdAsync(request.UserId);
    if (user == null)
        return TransactionErrors.InvalidUser;

    var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
    if (category == null)
        return TransactionErrors.InvalidCategory;

    var paymentMethod = await _paymentMethodRepository.GetByIdAsync(request.PaymentMethodId);
    if (paymentMethod == null)
        return TransactionErrors.InvalidPaymentMethod;

    Saving? saving = null;
if (request.SavingId.HasValue)
{
    Console.WriteLine($"[SAVING_VALIDATION] SavingId provided: {request.SavingId.Value}");
    
    if (request.Type != TransactionType.SAVING)
    {
        Console.WriteLine($"[SAVING_VALIDATION] ❌ Invalid transaction type. Expected: {TransactionType.SAVING}, Got: {request.Type}");
        return SavingErrors.InvalidTransactionType;
    }
    
    Console.WriteLine($"[SAVING_VALIDATION] ✅ Transaction type is SAVING");

    saving = await _savingRepository.GetByIdAsync(request.SavingId.Value);
    if (saving == null)
    {
        Console.WriteLine($"[SAVING_VALIDATION] ❌ Saving not found with ID: {request.SavingId.Value}");
        return SavingErrors.NotFound;
    }
    
    Console.WriteLine($"[SAVING_VALIDATION] ✅ Saving found: ID={saving.Id}, Name={saving.Name}, CurrentAmount={saving.CurrentAmount}, TargetAmount={saving.TargetAmount}");
    Console.WriteLine($"[SAVING_VALIDATION] Total users in saving: {saving.UserSavings.Count}");

    var userInSaving = saving.UserSavings
        .FirstOrDefault(us => us.UserId == request.UserId);

    if (userInSaving == null)
    {
        Console.WriteLine($"[SAVING_VALIDATION] ❌ User {request.UserId} is not part of saving {saving.Id}");
        Console.WriteLine($"[SAVING_VALIDATION] Users in saving: {string.Join(", ", saving.UserSavings.Select(us => us.UserId))}");
        return SavingErrors.UserNotPartOfSaving;
    }
    
    Console.WriteLine($"[SAVING_VALIDATION] ✅ User {request.UserId} is part of saving {saving.Id}");
    Console.WriteLine($"[SAVING_VALIDATION] User contributed so far: {userInSaving.ContributedAmount}");
}
else
{
    Console.WriteLine($"[SAVING_VALIDATION] No SavingId provided - this will be a regular transaction");
}

    var transaction = request.ToModel();
    await _transactionRepository.AddAsync(transaction);
    await _transactionRepository.SaveChangesAsync();

    Console.WriteLine($"[TRANSACTION] Created transaction: ID={transaction.Id}, UserID={request.UserId}, Amount={request.Amount}, Type={request.Type}, SavingID={request.SavingId}");

    var amountChange = GetAmountChangeByType(request.Type, request.Amount);
    Console.WriteLine($"[AMOUNT_CHANGE] Calculated amountChange: {amountChange}");

    var categoryResult = await _categoryService.UpdateCategoryAsync(
        request.CategoryId,
        amountChange,
        cancellationToken);

    if (categoryResult.IsError)
    {
        Console.WriteLine($"[ERROR] Failed to update category {request.CategoryId}: {categoryResult.Errors.First().Description}");
        return Error.Failure("Transaction.CategoryUpdateFailed", "Failed to update category");
    }

    Console.WriteLine($"[CATEGORY] Successfully updated category {request.CategoryId}");

    // ✅ If SavingId is provided, update the saving instead of budget
    if (request.SavingId.HasValue && saving != null)
    {
        Console.WriteLine($"[SAVING] Updating saving {saving.Id} with amount: {request.Amount}");
        
        // Update the saving's current amount directly
        saving.CurrentAmount += request.Amount;
        Console.WriteLine($"[SAVING] New saving amount: {saving.CurrentAmount}/{saving.TargetAmount}");
        
        // Update the user's contributed amount in this saving
        var userSaving = saving.UserSavings.FirstOrDefault(us => us.UserId == request.UserId);
        if (userSaving != null)
        {
            userSaving.ContributedAmount += request.Amount;
            Console.WriteLine($"[SAVING] User {request.UserId} contributed amount: {userSaving.ContributedAmount}");
        }

        // Save the changes to the saving
        await _savingRepository.UpdateAsync(saving);
        await _savingRepository.SaveChangesAsync();
        
        Console.WriteLine($"[SAVING] ✅ Successfully updated saving {saving.Id}");
        return transaction.ToDto();
    }

    // ✅ If no SavingId, update budget as usual
    var now = DateTime.UtcNow;
    Console.WriteLine($"[BUDGET] Looking for budget: CategoryID={request.CategoryId}, Month={now.Month}, Year={now.Year}");

    var allBudgets = await _budgetRepository.GetAllAsync();
    Console.WriteLine($"[BUDGET] Total budgets in database: {allBudgets.Count()}");

    var budget = allBudgets.FirstOrDefault(b =>
        b.CategoryId == request.CategoryId &&
        b.Month == now.Month &&
        b.Year == now.Year);

    if (budget == null)
    {
        Console.WriteLine($"[BUDGET] ⚠️  No budget found for CategoryID={request.CategoryId}, Month={now.Month}, Year={now.Year}");
        Console.WriteLine($"[BUDGET] Available budgets:");
        foreach (var b in allBudgets)
        {
            Console.WriteLine($"  - CategoryID={b.CategoryId}, Month={b.Month}, Year={b.Year}");
        }
        return transaction.ToDto();
    }

    Console.WriteLine($"[BUDGET] Found budget: ID={budget.Id}, CurrentAmount={budget.CurrentAmount}, Limit={budget.AmountLimit}");
    Console.WriteLine($"[BUDGET] Updating budget {budget.Id} with amountChange: {amountChange}");

    var budgetResult = await _budgetService.UpdateCurrentAmountAsync(
        budget.Id,
        amountChange,
        cancellationToken);

    if (budgetResult.IsError)
    {
        Console.WriteLine($"[ERROR] Failed to update budget {budget.Id}: {budgetResult.Errors.First().Description}");
        return Error.Failure("Transaction.BudgetUpdateFailed", "Failed to update budget");
    }

    Console.WriteLine($"[BUDGET] ✅ Successfully updated budget {budget.Id}");

    return transaction.ToDto();    
}

    public async Task<ErrorOr<TransactionDto>> UpdateTransactionAsync(
        int id,
        UpdateTransactionDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = _transactionValidator.ValidateUpdateTransaction(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return TransactionErrors.NotFound;

        var oldCategoryId = transaction.CategoryId;
        var oldAmount = transaction.Amount;
        var oldType = transaction.Type;
        var oldAmountChange = GetAmountChangeByType(oldType, oldAmount);

        if (request.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category == null)
                return TransactionErrors.InvalidCategory;

            transaction.CategoryId = request.CategoryId.Value;
        }

        if (request.PaymentMethodId.HasValue)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(request.PaymentMethodId.Value);
            if (paymentMethod == null)
                return TransactionErrors.InvalidPaymentMethod;

            transaction.PaymentMethodId = request.PaymentMethodId.Value;
        }

        if (request.Amount.HasValue)
            transaction.Amount = request.Amount.Value;

        if (request.Type.HasValue)
            transaction.Type = request.Type.Value;

        if (!string.IsNullOrEmpty(request.Description))
            transaction.Description = request.Description;

        if (request.Date.HasValue)
            transaction.Date = request.Date.Value;

        await _transactionRepository.UpdateAsync(transaction);
        await _transactionRepository.SaveChangesAsync();

        var newAmountChange = GetAmountChangeByType(transaction.Type, transaction.Amount);

        if (oldCategoryId != transaction.CategoryId)
        {
            var removeResult = await _categoryService.UpdateCategoryAsync(
                oldCategoryId,
                -oldAmountChange,
                cancellationToken);

            if (removeResult.IsError)
                return Error.Failure("Transaction.CategoryUpdateFailed", "Failed to update old category");

            var addResult = await _categoryService.UpdateCategoryAsync(
                transaction.CategoryId,
                newAmountChange,
                cancellationToken);

            if (addResult.IsError)
                return Error.Failure("Transaction.CategoryUpdateFailed", "Failed to update new category");
        }
        else if (oldAmountChange != newAmountChange)
        {
            var updateResult = await _categoryService.UpdateCategoryAsync(
                transaction.CategoryId,
                newAmountChange - oldAmountChange,
                cancellationToken);

            if (updateResult.IsError)
                return Error.Failure("Transaction.CategoryUpdateFailed", "Failed to update category");
        }

        var now = DateTime.UtcNow;
        var allBudgets = await _budgetRepository.GetAllAsync();

        if (oldCategoryId != transaction.CategoryId)
        {
            var oldBudget = allBudgets.FirstOrDefault(b =>
                b.CategoryId == oldCategoryId &&
                b.Month == now.Month &&
                b.Year == now.Year);

            if (oldBudget != null)
            {
                await _budgetService.UpdateCurrentAmountAsync(
                    oldBudget.Id,
                    -oldAmountChange,
                    cancellationToken);
            }

            var newBudget = allBudgets.FirstOrDefault(b =>
                b.CategoryId == transaction.CategoryId &&
                b.Month == now.Month &&
                b.Year == now.Year);

            if (newBudget != null)
            {
                await _budgetService.UpdateCurrentAmountAsync(
                    newBudget.Id,
                    newAmountChange,
                    cancellationToken);
            }
        }
        else if (oldAmountChange != newAmountChange)
        {
            var budget = allBudgets.FirstOrDefault(b =>
                b.CategoryId == transaction.CategoryId &&
                b.Month == now.Month &&
                b.Year == now.Year);

            if (budget != null)
            {
                await _budgetService.UpdateCurrentAmountAsync(
                    budget.Id,
                    newAmountChange - oldAmountChange,
                    cancellationToken);
            }
        }

        return transaction.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteTransactionAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return TransactionErrors.NotFound;

        var categoryId = transaction.CategoryId;
        var amount = transaction.Amount;
        var type = transaction.Type;
        var amountChange = GetAmountChangeByType(type, amount);

        await _transactionRepository.DeleteAsync(transaction);
        await _transactionRepository.SaveChangesAsync();

        await _categoryService.UpdateCategoryAsync(
            categoryId,
            -amountChange,
            cancellationToken);

        var now = DateTime.UtcNow;
        var allBudgets = await _budgetRepository.GetAllAsync();
        var budget = allBudgets.FirstOrDefault(b =>
            b.CategoryId == categoryId &&
            b.Month == now.Month &&
            b.Year == now.Year);

        if (budget != null)
        {
            await _budgetService.UpdateCurrentAmountAsync(
                budget.Id,
                -amountChange,
                cancellationToken);
        }

        return Result.Success;
    }

    private decimal GetAmountChangeByType(TransactionType type, decimal amount)
    {
        return type switch
        {
            TransactionType.INCOME => amount,
            TransactionType.EXPENSE => -amount,
            TransactionType.SAVING => -amount,
            _ => 0
        };
    }
}