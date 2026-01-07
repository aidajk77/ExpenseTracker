using Contracts.DTOs.Saving;
using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Savings.Interfaces.Application;
using Domain.Entities;
using api.Mappers;
using Domain.Enums;
using Domain.Errors;


namespace SampleCkWebApp.Application.Savings;

public class SavingService : ISavingService
{
    private readonly IRepository<Saving> _savingRepository;
    private readonly IRepository<User> _userRepository;
    private readonly SavingValidator _savingValidator;

    public SavingService(
        IRepository<Saving> savingRepository,
        IRepository<User> userRepository,
        SavingValidator savingValidator)
    {
        _savingRepository = savingRepository;
        _userRepository = userRepository;
        _savingValidator = savingValidator;
    }

    public async Task<ErrorOr<IEnumerable<SavingDto>>> GetAllSavingsAsync(CancellationToken cancellationToken = default)
    {
        var savings = await _savingRepository.GetAllAsync();
        return savings.Select(s => s.ToDto()).ToList();
    }

    public async Task<ErrorOr<SavingDto>> GetSavingByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var saving = await _savingRepository.GetByIdAsync(id);
        if (saving == null)
            return SavingErrors.NotFound;

        return saving.ToDto();
    }

    public async Task<ErrorOr<SavingDto>> CreateSavingAsync(CreateSavingDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _savingValidator.ValidateCreateSaving(request);
        if (validationResult.IsError)
            return validationResult.Errors;
            
        //  Validate all users exist
        var users = await _userRepository.GetAllAsync();
        foreach (var userId in request.UserIds)
        {
            if (!users.Any(u => u.Id == userId))
                return UserErrors.NotFound;
        }

        // Create saving
        var saving = new Saving
        {
            Name = request.Name,
            Description = request.Description,
            TargetAmount = request.TargetAmount,
            CurrentAmount = 0,
            TargetDate = request.TargetDate,
            Status = SavingStatus.Active,
            CreatedAt = DateTime.UtcNow,
            UserSavings = new List<Domain.Entities.UserSaving>()
        };

        // Add users to saving
        foreach (var userId in request.UserIds)
        {
            saving.UserSavings.Add(new Domain.Entities.UserSaving
            {
                UserId = userId,
                ContributedAmount = 0,
                JoinedAt = DateTime.UtcNow
            });
        }

        await _savingRepository.AddAsync(saving);
        await _savingRepository.SaveChangesAsync();

        return saving.ToDto();
    }

    public async Task<ErrorOr<SavingDto>> UpdateSavingAsync(int id, UpdateSavingDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _savingValidator.ValidateUpdateSaving(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var saving = await _savingRepository.GetByIdAsync(id);
        if (saving == null)
            return SavingErrors.NotFound;

        // Update Name if provided
        if (!string.IsNullOrEmpty(request.Name))
            saving.Name = request.Name;

        // Update Description if provided
        if (request.Description != null)
            saving.Description = request.Description;

        // Update TargetAmount if provided
        if (request.TargetAmount.HasValue)
        {
            if (request.TargetAmount.Value < saving.CurrentAmount)
                return SavingErrors.TargetAmountBelowCurrent;

            saving.TargetAmount = request.TargetAmount.Value;
        }
        // Update TargetDate if provided
        if (request.TargetDate.HasValue)
            saving.TargetDate = request.TargetDate.Value;

        // Update Status if provided
        if (request.Status.HasValue)
            saving.Status = request.Status.Value;
        // Update UserIds if provided
        if (request.UserIds != null && request.UserIds.Count > 0)
        {
            // Validate all users exist
            var users = await _userRepository.GetAllAsync();
            foreach (var userId in request.UserIds)
            {
                if (!users.Any(u => u.Id == userId))
                    return UserErrors.NotFound;
            }

            // Get current user IDs
            var currentUserIds = saving.UserSavings
                .Select(us => us.UserId)
                .ToList();

            // Find users to add
            var usersToAdd = request.UserIds
                .Where(uid => !currentUserIds.Contains(uid))
                .ToList();

            // Find users to remove
            var usersToRemove = currentUserIds
                .Where(uid => !request.UserIds.Contains(uid))
                .ToList();
            
            // Add new users
            foreach (var userId in usersToAdd)
            {
                saving.UserSavings.Add(new Domain.Entities.UserSaving
                {
                    UserId = userId,
                    ContributedAmount = 0,
                    JoinedAt = DateTime.UtcNow
                });
            }

            // Remove users
            foreach (var userId in usersToRemove)
            {
                var userSaving = saving.UserSavings
                    .FirstOrDefault(us => us.UserId == userId);

                if (userSaving != null)
                    saving.UserSavings.Remove(userSaving);
            }
        }

        await _savingRepository.UpdateAsync(saving);
        await _savingRepository.SaveChangesAsync();

        return saving.ToDto();

    }

    public async Task<ErrorOr<Success>> DeleteSavingAsync(int id, CancellationToken cancellationToken = default)
    {
        var saving = await _savingRepository.GetByIdAsync(id);
        if (saving == null)
            return SavingErrors.NotFound;

        await _savingRepository.DeleteAsync(saving);
        await _savingRepository.SaveChangesAsync();

        return Result.Success;
    }
}
