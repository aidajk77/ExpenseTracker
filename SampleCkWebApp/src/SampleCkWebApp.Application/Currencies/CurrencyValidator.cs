using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Currency;
using Domain.Errors;
using ErrorOr;

namespace SampleCkWebApp.Application.Currencies
{
    public class CurrencyValidator
    {
        public ErrorOr<Success> ValidateCreateCurrency(CreateCurrencyDto request)
        {
            var errors = new List<Error>();

            //  Validate Code
            if (string.IsNullOrWhiteSpace(request.Code))
                errors.Add(CurrencyErrors.CodeRequired);
            else if (request.Code.Length != 3)
                errors.Add(CurrencyErrors.InvalidCode);
            else if (!request.Code.All(char.IsLetter))
                errors.Add(CurrencyErrors.InvalidCode);

            //  Validate Name
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add(CurrencyErrors.NameRequired);
            else if (request.Name.Length > 50)
                errors.Add(CurrencyErrors.InvalidName);

            //  Validate Symbol
            if (string.IsNullOrWhiteSpace(request.Symbol))
                errors.Add(CurrencyErrors.SymbolRequired);
            else if (request.Symbol.Length > 5)
                errors.Add(CurrencyErrors.InvalidSymbol);

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateUpdateCurrency(UpdateCurrencyDto request)
        {
            var errors = new List<Error>();

            //  Validate Code if provided
            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                if (request.Code.Length != 3)
                    errors.Add(CurrencyErrors.InvalidCode);
                else if (!request.Code.All(char.IsLetter))
                    errors.Add(CurrencyErrors.InvalidCode);
            }

            //  Validate Name if provided
            if (!string.IsNullOrWhiteSpace(request.Name) && request.Name.Length > 50)
                errors.Add(CurrencyErrors.InvalidName);

            //  Validate Symbol if provided
            if (!string.IsNullOrWhiteSpace(request.Symbol) && request.Symbol.Length > 5)
                errors.Add(CurrencyErrors.InvalidSymbol);

            return errors.Count > 0 ? errors : Result.Success;
        }
    }
}