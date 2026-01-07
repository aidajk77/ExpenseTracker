using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleCkWebApp.Application.Budget;
using SampleCkWebApp.Application.Budgets.Interfaces.Application;

namespace SampleCkWebApp.Application.Budgets;

public static class DependencyInjection
{
    public static IServiceCollection AddBudgetApplication(this IServiceCollection services)
    {
        services.AddScoped<IBudgetService, BudgetService>();

        services.AddScoped<BudgetValidator>();
        return services;
    }
}