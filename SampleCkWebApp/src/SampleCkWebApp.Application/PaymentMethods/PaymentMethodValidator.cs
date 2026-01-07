using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.PaymentMethod;
using Domain.Errors;
using ErrorOr;

namespace SampleCkWebApp.Application.PaymentMethods
{
    public class PaymentMethodValidator
    {
        public ErrorOr<Success> ValidateCreatePaymentMethod(CreatePaymentMethodDto request)
        {
            var errors = new List<Error>();

            //  Validate Name
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add(PaymentMethodErrors.NameRequired);
            else if (request.Name.Length > 50)
                errors.Add(PaymentMethodErrors.InvalidName);
            else if (request.Name.Length < 1)
                errors.Add(PaymentMethodErrors.InvalidName);

            //  Validate Description length if provided
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 255)
                errors.Add(PaymentMethodErrors.InvalidDescription);

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateUpdatePaymentMethod(UpdatePaymentMethodDto request)
        {
            var errors = new List<Error>();

            //  Validate Name if provided
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                if (request.Name.Length > 50)
                    errors.Add(PaymentMethodErrors.InvalidName);
                else if (request.Name.Length < 1)
                    errors.Add(PaymentMethodErrors.InvalidName);
            }

            //  Validate Description length if provided
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 255)
                errors.Add(PaymentMethodErrors.InvalidDescription);

            return errors.Count > 0 ? errors : Result.Success;
        }
    }
}