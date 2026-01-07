using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Transaction.Interfaces.Application;
using Contracts.DTOs.Transaction;
using Domain.Entities;
using api.Mappers;
using Domain.Enums;
using Domain.Errors;

namespace SampleCkWebApp.Application.Transaction;

public class TransactionService : ITransactionService
{
    private readonly IRepository<Domain.Entities.Transaction> _transactionRepository;
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;
    private readonly IRepository<Domain.Entities.PaymentMethod> _paymentMethodRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Saving> _savingRepository;
    private readonly TransactionValidator _transactionValidator;

    public TransactionService(
        IRepository<Domain.Entities.Transaction> transactionRepository,
        IRepository<Domain.Entities.Category> categoryRepository,
        IRepository<Domain.Entities.PaymentMethod> paymentMethodRepository,
        IRepository<User> userRepository,
        IRepository<Saving> savingRepository,
        TransactionValidator transactionValidator)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _userRepository = userRepository;
        _savingRepository = savingRepository;
        _transactionValidator = transactionValidator;
    }


    public async Task<ErrorOr<IEnumerable<TransactionDto>>> GetAllTransactionsAsync(CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionRepository.GetAllAsync();
        return transactions.Select(t => t.ToDto()).ToList();
    }

    public async Task<ErrorOr<TransactionDto>> GetTransactionByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return TransactionErrors.NotFound;

        return transaction.ToDto();
    }

    public async Task<ErrorOr<TransactionDto>> CreateTransactionAsync(CreateTransactionDto request, CancellationToken cancellationToken = default)
    {   
        //  Validate input using validator
        var validationResult = _transactionValidator.ValidateCreateTransaction(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        // Validate User exists
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            return TransactionErrors.InvalidUser;

        // Validate Category exists
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            return TransactionErrors.InvalidCategory;

        // Validate PaymentMethod exists
        var paymentMethod = await _paymentMethodRepository.GetByIdAsync(request.PaymentMethodId);
        if (paymentMethod == null)
            return TransactionErrors.InvalidPaymentMethod;

        Saving? saving = null;
        if (request.SavingId.HasValue)
        {
            // Only SAVING type transactions can be linked to Saving
            if (request.Type != TransactionType.SAVING)
                return SavingErrors.InvalidTransactionType;

            saving = await _savingRepository.GetByIdAsync(request.SavingId.Value);
            if (saving == null)
                return SavingErrors.NotFound;

            // Verify user is part of this saving
            var userInSaving = saving.UserSavings
                .FirstOrDefault(us => us.UserId == request.UserId);

            if (userInSaving == null)
                return SavingErrors.UserNotPartOfSaving;
        }

            var transaction = request.ToModel();
            
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            return transaction.ToDto();    
    }

    public async Task<ErrorOr<TransactionDto>> UpdateTransactionAsync(int id, UpdateTransactionDto request, CancellationToken cancellationToken = default)
    {

        var validationResult = _transactionValidator.ValidateUpdateTransaction(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return TransactionErrors.NotFound;

        // Validate Category if provided
        if (request.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category == null)
                return TransactionErrors.InvalidCategory;

            transaction.CategoryId = request.CategoryId.Value;
        }

        // Validate PaymentMethod if provided
        if (request.PaymentMethodId.HasValue)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(request.PaymentMethodId.Value);
            if (paymentMethod == null)
                return TransactionErrors.InvalidPaymentMethod;

            transaction.PaymentMethodId = request.PaymentMethodId.Value;
        }

        // Update only provided fields
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

        return transaction.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteTransactionAsync(int id, CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return TransactionErrors.NotFound;

        await _transactionRepository.DeleteAsync(transaction);
        await _transactionRepository.SaveChangesAsync();

        return Result.Success;
    }
}
