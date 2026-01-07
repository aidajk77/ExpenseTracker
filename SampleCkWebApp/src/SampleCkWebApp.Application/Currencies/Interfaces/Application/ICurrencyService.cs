using ErrorOr;
using Domain.Entities;
using Contracts.DTOs.Currency;

namespace SampleCkWebApp.Application.Currencies.Interfaces.Application;

public interface ICurrencyService
{
    Task<ErrorOr<IEnumerable<CurrencyDto>>> GetAllCurrenciesAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<CurrencyDto>> GetCurrencyByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<CurrencyDto>> CreateCurrencyAsync(CreateCurrencyDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<CurrencyDto>> UpdateCurrencyAsync(int id, UpdateCurrencyDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeleteCurrencyAsync(int id, CancellationToken cancellationToken = default);
}