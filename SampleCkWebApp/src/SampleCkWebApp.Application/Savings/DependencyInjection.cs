using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Savings;
using SampleCkWebApp.Application.Savings.Interfaces.Application;


public static class DependencyInjection
{
    public static IServiceCollection AddSavingApplication(this IServiceCollection services)
    {
        services.AddScoped<ISavingService, SavingService>();

        services.AddScoped<SavingValidator>();
        
        return services;
    }
}
