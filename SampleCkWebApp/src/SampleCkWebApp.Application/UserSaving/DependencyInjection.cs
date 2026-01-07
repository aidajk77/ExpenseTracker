using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.UserSaving.Interfaces.Application;

namespace SampleCkWebApp.Application.UserSaving;

public static class DependencyInjection
{
    public static IServiceCollection AddUserSavingApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserSavingService, UserSavingService>();
        
        services.AddScoped<UserSavingValidator>();
        
        return services;
    }
}
