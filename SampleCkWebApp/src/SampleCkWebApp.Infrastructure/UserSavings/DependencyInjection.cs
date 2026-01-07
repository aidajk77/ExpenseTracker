using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.UserSavings.Options;
using SampleCkWebApp.Infrastructure.UserSavings.Repositories;

namespace SampleCkWebApp.Infrastructure.UserSavings;

public static class DependencyInjection
{
    public static IServiceCollection AddUserSavingsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var userSavingOptions = configuration.GetUserSavingOptions();
        services.TryAddUserSavingOptions(userSavingOptions);
        
        services.AddScoped<IRepository<UserSaving>, UserSavingRepository>();
        
        return services;
    }
}
