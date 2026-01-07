using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Budget;
using Domain.Errors;
using ErrorOr;

namespace SampleCkWebApp.Application.Budget
{
    public class BudgetValidator
    {
        public ErrorOr<Success> ValidateCreateBudget(CreateBudgetDto request)
        {
            var errors = new List<Error>();

            //  Validate CategoryId
            if (request.CategoryId <= 0)
                errors.Add(BudgetErrors.InvalidCategory);

            //  Validate AmountLimit
            if (request.AmountLimit <= 0)
                errors.Add(BudgetErrors.InvalidAmountLimit);

            //  Validate Month
            if (request.Month < 1 || request.Month > 12)
                errors.Add(BudgetErrors.InvalidMonth);

            //  Validate Year
            if (request.Year < 2000 || request.Year > 3000)
                errors.Add(BudgetErrors.InvalidYear);

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateUpdateBudget(UpdateBudgetDto request)
        {
            var errors = new List<Error>();

            //  Validate AmountLimit if provided
            if (request.AmountLimit.HasValue && request.AmountLimit <= 0)
                errors.Add(BudgetErrors.InvalidAmountLimit);

            return errors.Count > 0 ? errors : Result.Success;
        }
    }
}