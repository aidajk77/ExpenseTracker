using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Savings.Options;
using SampleCkWebApp.Infrastructure.Savings.Repositories;

namespace SampleCkWebApp.Infrastructure.Savings;

public static class DependencyInjection
{
    public static IServiceCollection AddSavingsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var savingOptions = configuration.GetSavingOptions();
        services.TryAddSavingOptions(savingOptions);
        
        services.AddScoped<IRepository<Saving>, SavingRepository>();
        
        return services;
    }
}
