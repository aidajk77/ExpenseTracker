using Contracts.DTOs.Saving;
using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Savings.Interfaces.Application;
using Domain.Entities;
using api.Mappers;
using Domain.Enums;
using Domain.Errors;
using SampleCkWebApp.Application.Savings.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Users.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Currencies.Interfaces.Infrastructure;


namespace SampleCkWebApp.Application.Savings;

public class SavingService : ISavingService
{
    private readonly ISavingRepository _savingRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly SavingValidator _savingValidator;
    private const int USD_CURRENCY_ID = 4;

    public SavingService(
        ISavingRepository savingRepository,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        SavingValidator savingValidator)
    {
        _savingRepository = savingRepository;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _savingValidator = savingValidator;
    }

    private async Task<decimal> ConvertFromUSDToUserCurrencyAsync(
        decimal amountInUSD,
        int userCurrencyId)
    {
        // If user currency is USD, no conversion needed
        if (userCurrencyId == USD_CURRENCY_ID)
            return amountInUSD;

        // Get exchange rate from Currency table
        var userCurrency = await _currencyRepository.GetByIdAsync(userCurrencyId);
        
        if (userCurrency == null || userCurrency.ExchangeRate <= 0)
            return amountInUSD; // Fallback if currency not found

        // ExchangeRate column stores: 1 USD = X currency
        return amountInUSD * userCurrency.ExchangeRate;
    }

    private async Task<decimal> ConvertFromUserCurrencyToUSDAsync(
        decimal amountInUserCurrency,
        int userCurrencyId)
    {
        // If user currency is USD, no conversion needed
        if (userCurrencyId == USD_CURRENCY_ID)
            return amountInUserCurrency;

        // Get exchange rate from Currency table
        var userCurrency = await _currencyRepository.GetByIdAsync(userCurrencyId);
        
        if (userCurrency == null || userCurrency.ExchangeRate <= 0)
            return amountInUserCurrency; // Fallback if currency not found

        // ExchangeRate column stores: 1 USD = X currency
        // Reverse: amount_in_user_currency / exchange_rate = amount_in_usd
        return amountInUserCurrency / userCurrency.ExchangeRate;
    }

    public async Task<ErrorOr<IEnumerable<SavingDto>>> GetAllSavingsAsync(CancellationToken cancellationToken = default)
    {
        var savings = await _savingRepository.GetAllAsync();
        return savings.Select(s => s.ToDto()).ToList();
    }

    public async Task<ErrorOr<IEnumerable<SavingDto>>> GetUserSavingsAsync(
        int userId, 
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return UserErrors.NotFound;
        var userSavings = await _savingRepository.GetUserSavingsAsync(userId);

        var savingDtos = new List<SavingDto>();
        foreach (var saving in userSavings)
        {
            var dto = saving.ToDto();
            
            //  Convert target and current amounts
            dto.TargetAmount = await ConvertFromUSDToUserCurrencyAsync(
                saving.TargetAmount,
                user.CurrencyId);
            dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                saving.CurrentAmount,
                user.CurrencyId);

            //  Convert contributor amounts
            if (dto.Contributors != null)
            {
                foreach (var contributor in dto.Contributors)
                {
                    contributor.ContributedAmount = await ConvertFromUSDToUserCurrencyAsync(
                        contributor.ContributedAmount,
                        user.CurrencyId);
                }
            }

            savingDtos.Add(dto);
        }
        
        return savingDtos;
    }

    public async Task<ErrorOr<IEnumerable<SavingDto>>> GetUserNonCompletedSavingsAsync(
        int userId, 
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return UserErrors.NotFound;

        var userNonCompletedSavings = await _savingRepository.GetUserNonCompletedSavingsAsync(userId);

        var savingDtos = new List<SavingDto>();
        foreach (var saving in userNonCompletedSavings)
        {
            var dto = saving.ToDto();
            
            //  Convert target and current amounts
            dto.TargetAmount = await ConvertFromUSDToUserCurrencyAsync(
                saving.TargetAmount,
                user.CurrencyId);
            dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                saving.CurrentAmount,
                user.CurrencyId);

            //  Convert contributor amounts
            if (dto.Contributors != null)
            {
                foreach (var contributor in dto.Contributors)
                {
                    contributor.ContributedAmount = await ConvertFromUSDToUserCurrencyAsync(
                        contributor.ContributedAmount,
                        user.CurrencyId);
                }
            }

            savingDtos.Add(dto);
        }
            
        return savingDtos;
    }

    public async Task<ErrorOr<SavingDto>> GetSavingByIdAsync(int id, int userId, CancellationToken cancellationToken = default)
    {
        var saving = await _savingRepository.GetByIdAsync(id);
        if (saving == null)
            return SavingErrors.NotFound;

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return UserErrors.NotFound;
        
        var userBelongsToSaving = await UserBelongsToSavingAsync(id, userId);
        if (!userBelongsToSaving)
            return Error.Forbidden("Saving.Forbidden", "You don't have permission to this saving");


        var dto = saving.ToDto();

        dto.TargetAmount = await ConvertFromUSDToUserCurrencyAsync(
            saving.TargetAmount,
            user.CurrencyId);
    
        dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
            saving.CurrentAmount,
            user.CurrencyId);

        if (dto.Contributors != null)
        {
            foreach (var contributor in dto.Contributors)
            {
                contributor.ContributedAmount = await ConvertFromUSDToUserCurrencyAsync(
                    contributor.ContributedAmount,
                    user.CurrencyId);
            }
        }

        return dto;
    }

    public async Task<ErrorOr<SavingDto>> CreateSavingAsync(CreateSavingDto request, int userId, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _savingValidator.ValidateCreateSaving(request);
        if (validationResult.IsError)
            return validationResult.Errors;
        
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return UserErrors.NotFound;
        

        //  Validate all users exist
        var existingUserIds = await _savingRepository.GetExistingUserIdsAsync(request.UserIds);
        var missingUsers = request.UserIds
            .Except(existingUserIds.ToList())
            .ToList();
        
        if (missingUsers.Any())
            return UserErrors.NotFound;

        var targetAmountInUSD = await ConvertFromUserCurrencyToUSDAsync(
            request.TargetAmount,
            user.CurrencyId);

        var randomCode = GenerateCode();

        // Create saving
        var saving = new Saving
    {
        Name = request.Name,
        Code = randomCode,
        Description = request.Description,
        TargetAmount = targetAmountInUSD,
        CurrentAmount = 0,
        TargetDate = request.TargetDate,
        Status = SavingStatus.Active,
        CreatedAt = DateTime.UtcNow,
        UserSavings = new List<Domain.Entities.UserSaving>()
    };

        // Add users to saving
        foreach (var savingUserId in request.UserIds)
        {
            saving.UserSavings.Add(new Domain.Entities.UserSaving
            {
                UserId = savingUserId,
                ContributedAmount = 0,
                JoinedAt = DateTime.UtcNow
            });
        }

        await _savingRepository.AddAsync(saving);
        await _savingRepository.SaveChangesAsync();

        var dto = saving.ToDto();
        dto.TargetAmount = await ConvertFromUSDToUserCurrencyAsync(
            saving.TargetAmount,
            user.CurrencyId);
        dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
            saving.CurrentAmount,
            user.CurrencyId);

        return dto;
    }

    public async Task<ErrorOr<SavingDto>> UpdateSavingAsync(int id, UpdateSavingDto request, int userId, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _savingValidator.ValidateUpdateSaving(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var saving = await _savingRepository.GetByIdAsync(id);
        if (saving == null)
            return SavingErrors.NotFound;

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return UserErrors.NotFound;
        
        var userBelongsToSaving = await UserBelongsToSavingAsync(id, userId);
        if (!userBelongsToSaving)
            return Error.Forbidden("Saving.Forbidden", "You don't have permission to this saving");

        // Update Name if provided
        if (!string.IsNullOrEmpty(request.Name))
            saving.Name = request.Name;

        // Update Description if provided
        if (request.Description != null)
            saving.Description = request.Description;

        // Update TargetAmount if provided
        if (request.TargetAmount.HasValue)
        {
            var targetAmountInUSD = await ConvertFromUserCurrencyToUSDAsync(
                request.TargetAmount.Value,
                user.CurrencyId);

            if (targetAmountInUSD < saving.CurrentAmount)
                return SavingErrors.TargetAmountBelowCurrent;

            saving.TargetAmount = targetAmountInUSD; 
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
            var existingUserIds = await _savingRepository.GetExistingUserIdsAsync(request.UserIds);
            
            var missingUsers = request.UserIds
                .Except(existingUserIds.ToList())
                .ToList();
            
            if (missingUsers.Any())
                return UserErrors.NotFound;

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
            foreach (var savingUserId in usersToAdd)
            {
                saving.UserSavings.Add(new Domain.Entities.UserSaving
                {
                    UserId = savingUserId,
                    ContributedAmount = 0,
                    JoinedAt = DateTime.UtcNow
                });
            }

            // Remove users
            foreach (var savingUserId in usersToRemove)
            {
                var userSaving = saving.UserSavings
                    .FirstOrDefault(us => us.UserId == savingUserId);

                if (userSaving != null)
                    saving.UserSavings.Remove(userSaving);
            }
        }

        await _savingRepository.UpdateAsync(saving);
        await _savingRepository.SaveChangesAsync();

        var dto = saving.ToDto();
        dto.TargetAmount = await ConvertFromUSDToUserCurrencyAsync(
            saving.TargetAmount,
            user.CurrencyId);
        dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
            saving.CurrentAmount,
            user.CurrencyId);

        if (dto.Contributors != null)
        {
            foreach (var contributor in dto.Contributors)
            {
                contributor.ContributedAmount = await ConvertFromUSDToUserCurrencyAsync(
                    contributor.ContributedAmount,
                    user.CurrencyId);
            }
        }

        return dto;

    }


    public async Task<ErrorOr<SavingDto>> AddSavingTransactionAsync(
        int savingId,
        int userId,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        var saving = await _savingRepository.GetByIdAsync(savingId);
        if (saving == null)
            return SavingErrors.NotFound;

        var validationResult = _savingValidator.ValidateAddSavingTransaction(saving, userId, amount);
        if (validationResult.IsError)
            return validationResult.Errors;

        var userSaving = saving.UserSavings.First(us => us.UserId == userId);
        
        saving.CurrentAmount += amount;
        userSaving.ContributedAmount += amount;

        if (saving.CurrentAmount >= saving.TargetAmount)
        {
            saving.Status = SavingStatus.Completed;
        }

        await _savingRepository.UpdateAsync(saving);
        await _savingRepository.SaveChangesAsync();

        var user = await _userRepository.GetByIdAsync(userId);
        var dto = saving.ToDto();
        
        if (user != null)
        {
            dto.TargetAmount = await ConvertFromUSDToUserCurrencyAsync(
                saving.TargetAmount,
                user.CurrencyId);
            dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                saving.CurrentAmount,
                user.CurrencyId);

            if (dto.Contributors != null)
            {
                foreach (var contributor in dto.Contributors)
                {
                    contributor.ContributedAmount = await ConvertFromUSDToUserCurrencyAsync(
                        contributor.ContributedAmount,
                        user.CurrencyId);
                }
            }
        }

        return dto;
    }

    public async Task<ErrorOr<Success>> RemoveSavingTransactionAsync(
        int savingId,
        int userId,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        var saving = await _savingRepository.GetByIdAsync(savingId);
        if (saving == null)
            return SavingErrors.NotFound;

        var validationResult = _savingValidator.ValidateRemoveSavingTransaction(saving, userId, amount);
        if (validationResult.IsError)
            return validationResult.Errors;

        var userSaving = saving.UserSavings.First(us => us.UserId == userId);
        
        saving.CurrentAmount -= amount;
        userSaving.ContributedAmount -= amount;

        if (saving.Status == SavingStatus.Completed && saving.CurrentAmount < saving.TargetAmount)
        {
            saving.Status = SavingStatus.Active;
        }

        await _savingRepository.UpdateAsync(saving);
        await _savingRepository.SaveChangesAsync();

        return Result.Success;
    }


    public async Task<ErrorOr<Success>> DeleteSavingAsync(int id, int userId, CancellationToken cancellationToken = default)
    {
        var saving = await _savingRepository.GetByIdAsync(id);
        if (saving == null)
            return SavingErrors.NotFound;

        var userBelongsToSaving = await UserBelongsToSavingAsync(id, userId);
        if (!userBelongsToSaving)
            return Error.Forbidden("Saving.Forbidden", "You don't have permission to this saving");

        await _savingRepository.DeleteAsync(saving);
        await _savingRepository.SaveChangesAsync();

        return Result.Success;
    }

    private string GenerateCode()
    {
        // Combine timestamp + GUID for uniqueness
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4);
        
        // Format: timestamp (8 chars) + guid (4 chars) = 12 chars total
        return $"{timestamp:X8}{guid}".ToUpper();
    }

    private async Task<bool> UserBelongsToSavingAsync(int savingId, int userId)
    {
        var saving = await _savingRepository.GetByIdAsync(savingId);
        if (saving == null)
            return false;

        // Check if user is in the UserSavings list
        return saving.UserSavings?.Any(us => us.UserId == userId) ?? false;
    }
}
