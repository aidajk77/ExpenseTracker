using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.UserSaving;
using Domain.Errors;
using ErrorOr;

namespace SampleCkWebApp.Application.UserSaving
{
    public class UserSavingValidator
    {
        public ErrorOr<Success> ValidateCreateUserSaving(CreateUserSavingDto request)
        {
            var errors = new List<Error>();

            //  Validate UserId
            if (request.UserId <= 0)
                errors.Add(UserSavingErrors.InvalidUserId);

            //  Validate SavingId
            if (request.SavingId <= 0)
                errors.Add(UserSavingErrors.InvalidSavingId);

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateUpdate(UpdateUserSavingDto request)
        {
            var errors = new List<Error>();

            //  Validate ContributedAmount if provided
            if (request.ContributedAmount.HasValue && request.ContributedAmount < 0)
                errors.Add(UserSavingErrors.InvalidAmount);

            return errors.Count > 0 ? errors : Result.Success;
        }
        
    }
}