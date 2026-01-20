using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.UserSaving.Interfaces.Application;
using Contracts.DTOs.UserSaving;
using Domain.Entities;
using api.Mappers;
using Domain.Errors;

namespace SampleCkWebApp.Application.UserSaving;

public class UserSavingService : IUserSavingService
{
    private readonly IRepository<Saving> _savingRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Domain.Entities.UserSaving> _userSavingRepository;
    private readonly UserSavingValidator _userSavingValidator;

    public UserSavingService(
        IRepository<Saving> savingRepository,
        IRepository<User> userRepository,
        IRepository<Domain.Entities.UserSaving> userSavingRepository,
        UserSavingValidator userSavingValidator)
    {
        _savingRepository = savingRepository;
        _userRepository = userRepository;
        _userSavingRepository = userSavingRepository;
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
        var userSaving = await _userSavingRepository.GetByIdAsync(userId);
        if (userSaving == null)
            return UserSavingErrors.NotFound;

        if (userSaving.SavingId != savingId)
            return UserSavingErrors.NotFound;

        var dto = userSaving.ToDto(userSaving.Saving?.CurrentAmount ?? 0);
        return dto;
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
        var existingUserSaving = saving.UserSavings
            .FirstOrDefault(us => us.UserId == request.UserId);

        if (existingUserSaving != null)
            return UserSavingErrors.AlreadyExists;

        // Create using mapper
        var userSaving = request.ToModel();

        await _userSavingRepository.AddAsync(userSaving);

        return userSaving.ToDto();
    }

    public async Task<ErrorOr<UserSavingDto>> UpdateUserSavingAsync(int userId, int savingId, UpdateUserSavingDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _userSavingValidator.ValidateUpdate(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        //  Find the user saving
        var userSavings = await _userSavingRepository.GetAllAsync();
        var userSaving = userSavings.FirstOrDefault(us => us.UserId == userId && us.SavingId == savingId);

        if (userSaving is null)  //  Use is null
            return UserSavingErrors.NotFound;  //  Use domain error

        //  Update only provided fields
        if (request.ContributedAmount.HasValue)
            userSaving.ContributedAmount = request.ContributedAmount.Value;

        await _userSavingRepository.UpdateAsync(userSaving);

        var saving = await _savingRepository.GetByIdAsync(savingId);
        return userSaving.ToDto(saving?.CurrentAmount ?? 0);
    }

    public async Task<ErrorOr<Success>> DeleteUserSavingAsync(int userId, int savingId, CancellationToken cancellationToken = default)
    {
        //  Find the user saving
        var userSavings = await _userSavingRepository.GetAllAsync();
        var userSaving = userSavings.FirstOrDefault(us => us.UserId == userId && us.SavingId == savingId);

        if (userSaving is null)  
            return UserSavingErrors.NotFound;  //  Use domain error

        await _userSavingRepository.DeleteAsync(userSaving);

        return Result.Success;
    }

}
