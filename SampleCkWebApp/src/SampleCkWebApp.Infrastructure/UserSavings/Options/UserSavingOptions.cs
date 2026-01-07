using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SampleCkWebApp.Infrastructure.UserSavings.Options;

public sealed class UserSavingOptions
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; init; }
    
    public static ValidateOptionsResult Validate(UserSavingOptions? options)
    {
        if (options is null)
        {
            return ValidateOptionsResult.Fail(
                $"Configuration section '{SectionName}' is null.");
        }
        
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            return ValidateOptionsResult.Fail(
                $"Property '{nameof(options.ConnectionString)}' is required.");
        }
        
        return ValidateOptionsResult.Success;
    }
}

public static class UserSavingOptionsExtensions
{
    public static IServiceCollection TryAddUserSavingOptions(this IServiceCollection services, UserSavingOptions? options = null)
    {
        var validationResult = UserSavingOptions.Validate(options);
        if (!validationResult.Succeeded)
        {
            throw new OptionsValidationException(UserSavingOptions.SectionName, typeof(UserSavingOptions), validationResult.Failures);
        }
        
        services.TryAddSingleton(options!);
        return services;
    }
    
    public static UserSavingOptions? GetUserSavingOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(UserSavingOptions.SectionName);
        if (!section.Exists())
        {
            return null;
        }
        
        UserSavingOptions options = new();
        section.Bind(options);
        return options;
    }
}
