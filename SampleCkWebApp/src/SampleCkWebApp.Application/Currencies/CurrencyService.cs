using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using Contracts.DTOs.Currency;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Currencies.Interfaces.Application;

namespace SampleCkWebApp.Application.Currencies
{
    public class CurrencyService : ICurrencyService
    {

        private readonly IRepository<Currency> _currencyRepository;
        private readonly CurrencyValidator _currencyValidator;

        public CurrencyService(
            IRepository<Currency> currencyRepository,
            CurrencyValidator currencyValidator)
        {
            _currencyRepository = currencyRepository;
            _currencyValidator = currencyValidator;
        }
        public async Task<ErrorOr<CurrencyDto>> CreateCurrencyAsync(CreateCurrencyDto request, CancellationToken cancellationToken = default)
        {
            //  Validate input using validator
            var validationResult = _currencyValidator.ValidateCreateCurrency(request);
            if (validationResult.IsError)
                return validationResult.Errors;
            
            //  Check for duplicate code
            var existingCurrencies = await _currencyRepository.GetAllAsync();
            if (existingCurrencies.Any(c => c.Code == request.Code))
                return CurrencyErrors.DuplicateCode;  // ✅ Use domain error

            var currency = request.ToModel();

            await _currencyRepository.AddAsync(currency);
            await _currencyRepository.SaveChangesAsync();

            return currency.ToDto();
        }

        public async Task<ErrorOr<Success>> DeleteCurrencyAsync(int id, CancellationToken cancellationToken = default)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            if (currency == null)
                throw new KeyNotFoundException($"Currency with ID {id} not found.");

            await _currencyRepository.DeleteAsync(currency);
            await _currencyRepository.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<ErrorOr<IEnumerable<CurrencyDto>>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            var currencies = await _currencyRepository.GetAllAsync();
            return currencies.Select(c => c.ToDto()).ToList();
        }

        public async Task<ErrorOr<CurrencyDto>> GetCurrencyByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            if (currency == null)
                return CurrencyErrors.NotFound;

            return currency.ToDto();
        }

        public async Task<ErrorOr<CurrencyDto>> UpdateCurrencyAsync(int id, UpdateCurrencyDto request, CancellationToken cancellationToken = default)
        {
            //  Validate input using validator
            var validationResult = _currencyValidator.ValidateUpdateCurrency(request);
            if (validationResult.IsError)
                return validationResult.Errors;

            var currency = await _currencyRepository.GetByIdAsync(id);
            if (currency == null)
                return CurrencyErrors.NotFound;

            //  Check for duplicate code if code is being updated
            if (!string.IsNullOrEmpty(request.Code) && request.Code != currency.Code)
            {
                var existingCurrencies = await _currencyRepository.GetAllAsync();
                if (existingCurrencies.Any(c => c.Code == request.Code))
                    return CurrencyErrors.DuplicateCode;  // ✅ Use domain error
            }

            if (!string.IsNullOrEmpty(request.Code))
                currency.Code = request.Code;

            if (!string.IsNullOrEmpty(request.Name))
                currency.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Symbol))
                currency.Symbol = request.Symbol;

            await _currencyRepository.UpdateAsync(currency);
            await _currencyRepository.SaveChangesAsync();

            return currency.ToDto();
        }
    }
}