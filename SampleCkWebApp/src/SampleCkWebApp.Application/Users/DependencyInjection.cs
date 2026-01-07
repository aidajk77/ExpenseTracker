using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Common;
using SampleCkWebApp.Application.Common.Interfaces.Application;
using SampleCkWebApp.Application.Users.Interfaces.Application;

namespace SampleCkWebApp.Application.Users;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddScoped<AuthValidator>();
        
        return services;
    }
}
