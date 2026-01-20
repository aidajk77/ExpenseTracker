using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Saving;
using Domain.Errors;
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
                errors.Add(SavingErrors.InvalidName);  //  Use domain error
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
                    errors.Add(SavingErrors.InvalidName);  //  Use domain error
            }

            //  Validate Description length if provided
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 500)
                errors.Add(SavingErrors.InvalidDescription);  //  Use domain error

            //  Validate TargetAmount if provided
            if (request.TargetAmount.HasValue && request.TargetAmount <= 0)
                errors.Add(SavingErrors.InvalidTargetAmount);

            //  Validate TargetDate if provided
            if (request.TargetDate.HasValue && request.TargetDate <= DateTime.UtcNow)
                errors.Add(SavingErrors.InvalidTargetDate);  //  Use domain error

            //  Validate Status if provided
            if (request.Status.HasValue && !Enum.IsDefined(typeof(Domain.Enums.SavingStatus), request.Status))
                errors.Add(SavingErrors.InvalidStatus);

            //  Validate UserIds if provided
            if (request.UserIds is not null)
            {
                if (request.UserIds.Count == 0)
                    errors.Add(SavingErrors.InvalidUsersEmpty);  //  Use domain error
                else if (request.UserIds.Any(id => id <= 0))
                    errors.Add(SavingErrors.InvalidUserIdValue);  //  Use domain error
            }

            return errors.Count > 0 ? errors : Result.Success;
        }
    }
}