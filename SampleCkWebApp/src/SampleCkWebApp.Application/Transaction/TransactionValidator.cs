using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Transaction;
using Domain.Errors;
using ErrorOr;

namespace SampleCkWebApp.Application.Transaction
{
    public class TransactionValidator
    {
        public ErrorOr<Success> ValidateCreateTransaction(CreateTransactionDto request)
        {
            var errors = new List<Error>();

            //  Validate UserId
            if (request.UserId <= 0)
                errors.Add(TransactionErrors.InvalidUser);

            //  Validate CategoryId
            if (request.CategoryId <= 0)
                errors.Add(TransactionErrors.InvalidCategory);

            //  Validate PaymentMethodId
            if (request.PaymentMethodId <= 0)
                errors.Add(TransactionErrors.InvalidPaymentMethod);

            //  Validate Type (enum)
            if (!Enum.IsDefined(typeof(Domain.Enums.TransactionType), request.Type))
                errors.Add(TransactionErrors.InvalidType);

            //  Validate Description length
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 255)
                errors.Add(TransactionErrors.DescriptionTooLong);

            //  Validate Date
            if (request.Date == default || request.Date > DateTime.UtcNow)
                errors.Add(TransactionErrors.InvalidDate);

            //  Validate SavingId if provided (optional)
            if (request.SavingId.HasValue && request.SavingId <= 0)
                errors.Add(TransactionErrors.InvalidSavingId);

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateUpdateTransaction(UpdateTransactionDto request)
        {
            var errors = new List<Error>();

            //  Validate CategoryId if provided
            if (request.CategoryId.HasValue && request.CategoryId <= 0)
                errors.Add(TransactionErrors.InvalidCategory);

            //  Validate PaymentMethodId if provided
            if (request.PaymentMethodId.HasValue && request.PaymentMethodId <= 0)
                errors.Add(TransactionErrors.InvalidPaymentMethod);

            //  Validate SavingId if provided
            if (request.SavingId.HasValue && request.SavingId <= 0)
                errors.Add(TransactionErrors.InvalidSavingId);

            //  Validate Amount if provided
            if (request.Amount.HasValue && request.Amount <= 0)
                errors.Add(TransactionErrors.InvalidAmount);

            //  Validate Type if provided
            if (request.Type.HasValue && !Enum.IsDefined(typeof(Domain.Enums.TransactionType), request.Type))
                errors.Add(TransactionErrors.InvalidType);

            //  Validate Description length if provided
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 255)
                errors.Add(TransactionErrors.DescriptionTooLong);
            
            //  Validate Date if provided
            if (request.Date.HasValue && (request.Date == default || request.Date > DateTime.UtcNow))
                errors.Add(TransactionErrors.InvalidDate);

            return errors.Count > 0 ? errors : Result.Success;
        }
    }
}