using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SampleCkWebApp.Infrastructure.Savings.Options;

public sealed class SavingOptions
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; init; }
    
    public static ValidateOptionsResult Validate(SavingOptions? options)
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

public static class SavingOptionsExtensions
{
    public static IServiceCollection TryAddSavingOptions(this IServiceCollection services, SavingOptions? options = null)
    {
        var validationResult = SavingOptions.Validate(options);
        if (!validationResult.Succeeded)
        {
            throw new OptionsValidationException(SavingOptions.SectionName, typeof(SavingOptions), validationResult.Failures);
        }
        
        services.TryAddSingleton(options!);
        return services;
    }
    
    public static SavingOptions? GetSavingOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(SavingOptions.SectionName);
        if (!section.Exists())
        {
            return null;
        }
        
        SavingOptions options = new();
        section.Bind(options);
        return options;
    }
}
