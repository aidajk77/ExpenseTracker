using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Saving;
using Domain.Errors;
using Domain.Entities;
using ErrorOr;

namespace SampleCkWebApp.Application.Savings
{
    public class SavingValidator
    {
        public ErrorOr<Success> ValidateCreateSaving(CreateSavingDto request)
        {
            var errors = new List<Error>();

            //  Validate Name
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add(SavingErrors.NameRequired);
            else if (request.Name.Length > 100)
                errors.Add(SavingErrors.InvalidName);
            else if (request.Name.Length < 1)
                errors.Add(SavingErrors.InvalidName);

            //  Validate Description length if provided
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 500)
                errors.Add(SavingErrors.InvalidDescription);

            //  Validate TargetAmount
            if (request.TargetAmount <= 0)
                errors.Add(SavingErrors.InvalidTargetAmount);
            
            //  Validate TargetDate if provided
            if (request.TargetDate.HasValue && request.TargetDate <= DateTime.UtcNow)
                errors.Add(SavingErrors.InvalidTargetDate);

            //  Validate UserIds
            if (request.UserIds is null || request.UserIds.Count == 0)
                errors.Add(SavingErrors.InvalidUsersEmpty);
            else if (request.UserIds.Any(id => id <= 0))
                errors.Add(SavingErrors.InvalidUserIdValue);

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateUpdateSaving(UpdateSavingDto request)
        {
            var errors = new List<Error>();

            //  Validate Name if provided
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                if (request.Name.Length > 100 || request.Name.Length < 1)
                    errors.Add(SavingErrors.InvalidName);
            }

            //  Validate Description length if provided
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 500)
                errors.Add(SavingErrors.InvalidDescription);

            //  Validate TargetAmount if provided
            if (request.TargetAmount.HasValue && request.TargetAmount <= 0)
                errors.Add(SavingErrors.InvalidTargetAmount);

            //  Validate TargetDate if provided
            if (request.TargetDate.HasValue && request.TargetDate <= DateTime.UtcNow)
                errors.Add(SavingErrors.InvalidTargetDate);

            //  Validate Status if provided
            if (request.Status.HasValue && !Enum.IsDefined(typeof(Domain.Enums.SavingStatus), request.Status))
                errors.Add(SavingErrors.InvalidStatus);

            //  Validate UserIds if provided
            if (request.UserIds is not null)
            {
                if (request.UserIds.Count == 0)
                    errors.Add(SavingErrors.InvalidUsersEmpty);
                else if (request.UserIds.Any(id => id <= 0))
                    errors.Add(SavingErrors.InvalidUserIdValue);
            }

            return errors.Count > 0 ? errors : Result.Success;
        }

        //  Validate adding transaction to saving
        public ErrorOr<Success> ValidateAddSavingTransaction(Saving saving, int userId, decimal amount)
        {
            var errors = new List<Error>();

            //  Validate user is part of this saving
            var userSaving = saving.UserSavings.FirstOrDefault(us => us.UserId == userId);
            if (userSaving == null)
                errors.Add(SavingErrors.UserNotPartOfSaving);

            //  Validate amount is positive
            if (amount <= 0)
                errors.Add(Error.Validation("Saving.InvalidAmount", "Amount must be greater than 0"));

            //  Validate that new total doesn't exceed target amount
            decimal newTotal = saving.CurrentAmount + amount;
            if (newTotal > saving.TargetAmount)
                errors.Add(Error.Validation(
                    "Saving.AmountExceedsTarget",
                    $"Transaction amount would exceed target. Current: {saving.CurrentAmount}, " +
                    $"Adding: {amount}, Target: {saving.TargetAmount}. " +
                    $"Maximum you can add: {saving.TargetAmount - saving.CurrentAmount}"));

            return errors.Count > 0 ? errors : Result.Success;
        }

        //  Validate removing transaction from saving
        public ErrorOr<Success> ValidateRemoveSavingTransaction(Saving saving, int userId, decimal amount)
        {
            var errors = new List<Error>();

            //  Validate user is part of this saving
            var userSaving = saving.UserSavings.FirstOrDefault(us => us.UserId == userId);
            if (userSaving == null)
                errors.Add(SavingErrors.UserNotPartOfSaving);

            //  Validate amount is positive
            if (amount <= 0)
                errors.Add(Error.Validation("Saving.InvalidAmount", "Amount must be greater than 0"));

            //  Validate there's enough in saving to remove
            if (saving.CurrentAmount < amount)
                errors.Add(Error.Validation("Saving.InsufficientAmount", "Cannot remove more than current amount"));

            //  Validate user has contributed enough
            if (userSaving != null && userSaving.ContributedAmount < amount)
                errors.Add(Error.Validation("Saving.InsufficientUserAmount", "User has not contributed that much"));

            return errors.Count > 0 ? errors : Result.Success;
        }
    }
}