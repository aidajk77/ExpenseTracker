using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleCkWebApp.Application.Currencies;
using SampleCkWebApp.Application.Currencies.Interfaces.Application;

namespace SampleCkWebApp.Application.Currencies;

public static class DependencyInjection
{
    public static IServiceCollection AddCurrencyApplication(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<CurrencyValidator>();
        return services;
    }
}