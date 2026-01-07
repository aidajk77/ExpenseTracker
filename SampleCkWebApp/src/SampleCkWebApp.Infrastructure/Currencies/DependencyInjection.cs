using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Infrastructure.Currencies.Options;
using SampleCkWebApp.Infrastructure.Currencies.Repositories;
using SampleCkWebApp.Infrastructure.Users.Options;

namespace SampleCkWebApp.Infrastructure.Currencies;

public static class DependencyInjection
{
    public static IServiceCollection AddCurrenciesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddCurrencyOptions(configuration.GetCurrencyOptions());
        
        services.TryAddScoped<IRepository<Currency>, CurrencyRepository>();
        
        return services;
    }
}

