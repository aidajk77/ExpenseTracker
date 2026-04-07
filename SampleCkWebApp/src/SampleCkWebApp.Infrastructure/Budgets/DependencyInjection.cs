using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleCkWebApp.Application.Budget.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Infrastructure.Budgets.Options;
using SampleCkWebApp.Infrastructure.Budgets.Repositories;
using SampleCkWebApp.Infrastructure.Users.Options;

namespace SampleCkWebApp.Infrastructure.Budgets;

public static class DependencyInjection
{
    public static IServiceCollection AddBudgetInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddBudgetOptions(configuration.GetBudgetOptions());
        
        services.TryAddScoped<IBudgetRepository, BudgetRepository>();
        
        return services;
    }
}

