using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Currencies.Interfaces.Infrastructure;

public interface ICurrencyRepository : IRepository<Currency>
{
    Task<bool> CurrencyCodeExistsAsync(string code);

    Task<Currency?> GetCurrencyByCodeAsync(string code);
}