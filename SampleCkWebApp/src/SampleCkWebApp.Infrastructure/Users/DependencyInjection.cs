using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Infrastructure.Users.Options;
using SampleCkWebApp.Infrastructure.Users.Repositories;

namespace SampleCkWebApp.Infrastructure.Users;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddUserOptions(configuration.GetUserOptions());
        
        services.TryAddScoped<IRepository<User>, UserRepository>();
        
        return services;
    }
}

