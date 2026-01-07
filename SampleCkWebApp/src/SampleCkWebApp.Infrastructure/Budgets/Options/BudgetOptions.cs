using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SampleCkWebApp.Infrastructure.Budgets.Options;

public sealed class BudgetOptions
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; init; }
    
    public static ValidateOptionsResult Validate(BudgetOptions? options)
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

public static class BudgetOptionsExtensions
{
    public static IServiceCollection TryAddBudgetOptions(this IServiceCollection services, BudgetOptions? options = null)
    {
        var validationResult = BudgetOptions.Validate(options);
        if (!validationResult.Succeeded)
        {
            throw new OptionsValidationException(BudgetOptions.SectionName, typeof(BudgetOptions), validationResult.Failures);
        }
        
        services.TryAddSingleton(options!);
        return services;
    }
    
    public static BudgetOptions? GetBudgetOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(BudgetOptions.SectionName);
        if (!section.Exists())
        {
            return null;
        }
        
        BudgetOptions options = new();
        section.Bind(options);
        return options;
    }
}