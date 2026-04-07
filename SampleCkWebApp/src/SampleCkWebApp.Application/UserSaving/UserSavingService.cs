using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.UserSaving.Interfaces.Application;
using Contracts.DTOs.UserSaving;
using Domain.Entities;
using api.Mappers;
using Domain.Errors;
using SampleCkWebApp.Application.UserSaving.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Savings.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Users.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Transaction.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.UserSaving;

public class UserSavingService : IUserSavingService
{
    private readonly ISavingRepository _savingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserSavingRepository _userSavingRepository;
    private readonly UserSavingValidator _userSavingValidator;

    public UserSavingService(
        ISavingRepository savingRepository,
        IUserRepository userRepository,
        IUserSavingRepository userSavingRepository,
        ITransactionRepository transactionRepository,
        UserSavingValidator userSavingValidator)
    {
        _savingRepository = savingRepository;
        _userRepository = userRepository;
        _userSavingRepository = userSavingRepository;
        _transactionRepository = transactionRepository;
        _userSavingValidator = userSavingValidator;
    }

    public async Task<ErrorOr<IEnumerable<UserSavingDto>>> GetAllUserSavingsAsync(CancellationToken cancellationToken = default)
    {
        var userSavings = await _userSavingRepository.GetAllAsync();
        return userSavings
            .Select(us => us.ToDto(us.Saving?.CurrentAmount ?? 0))
            .ToList();
    }

    public async Task<ErrorOr<UserSavingDto>> GetUserSavingByIdAsync(int userId, int savingId, CancellationToken cancellationToken = default)
    {
        var userSaving = await _userSavingRepository.GetUserSavingAsync(userId, savingId);
        
        if (userSaving == null)
            return UserSavingErrors.NotFound;

        return userSaving.ToDto(userSaving.Saving.CurrentAmount);
    }

    public async Task<ErrorOr<UserSavingDto>> CreateUserSavingAsync(CreateUserSavingDto request, CancellationToken cancellationToken = default)
    {

        //  Validate input using validator
        var validationResult = _userSavingValidator.ValidateCreateUserSaving(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        // Validate saving exists
        var saving = await _savingRepository.GetByIdAsync(request.SavingId);
        if (saving == null)
            return UserSavingErrors.SavingNotFound;

        // Validate user exists
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            return UserSavingErrors.UserNotFound;

        // Check if user already in saving
        var alreadyExists = await _userSavingRepository.UserExistsInSavingAsync(
            request.UserId, 
            request.SavingId);

        if (alreadyExists)
            return UserSavingErrors.AlreadyExists;

        // Create using mapper
        var userSaving = request.ToModel();

        await _userSavingRepository.AddAsync(userSaving);
        await _userSavingRepository.SaveChangesAsync();

        return userSaving.ToDto();
    }

    public async Task<ErrorOr<UserSavingDto>> UpdateUserSavingAsync(int userId, int savingId, UpdateUserSavingDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _userSavingValidator.ValidateUpdate(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        //  Find the user saving
        var userSaving = await _userSavingRepository.GetUserSavingAsync(userId, savingId);

        if (userSaving is null) 
            return UserSavingErrors.NotFound; 

        //  Update only provided fields
        if (request.ContributedAmount.HasValue)
            userSaving.ContributedAmount = request.ContributedAmount.Value;

        await _userSavingRepository.UpdateAsync(userSaving);
        await _userSavingRepository.SaveChangesAsync();

        return userSaving.ToDto(userSaving.Saving.CurrentAmount);
    }

    public async Task<ErrorOr<Success>> DeleteUserSavingAsync(int userId, int savingId, CancellationToken cancellationToken = default)
    {
        //  Find the user saving
        var userSaving = await _userSavingRepository.GetUserSavingAsync(userId, savingId);

        if (userSaving is null)  
            return UserSavingErrors.NotFound;  //  Use domain error

       try
        {
            //  Step 1: Get all transactions related to this saving
            var transactions = await _transactionRepository.GetBySavingIdAsync(savingId);

            //  Step 2: Delete each transaction
            foreach (var transaction in transactions)
            {
                await _transactionRepository.DeleteAsync(transaction);
            }
            await _transactionRepository.SaveChangesAsync();

            //  Step 3: Delete the user saving
            await _userSavingRepository.DeleteAsync(userSaving);
            await _userSavingRepository.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Failure("UserSaving.DeleteFailed", ex.Message);
        }
    }

}
