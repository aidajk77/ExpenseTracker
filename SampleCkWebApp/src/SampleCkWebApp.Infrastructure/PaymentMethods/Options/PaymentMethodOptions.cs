using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SampleCkWebApp.Infrastructure.PaymentMethods.Options;

public sealed class PaymentMethodOptions
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; init; }
    
    public static ValidateOptionsResult Validate(PaymentMethodOptions? options)
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

public static class PaymentMethodOptionsExtensions
{
    public static IServiceCollection TryAddPaymentMethodOptions(this IServiceCollection services, PaymentMethodOptions? options = null)
    {
        var validationResult = PaymentMethodOptions.Validate(options);
        if (!validationResult.Succeeded)
        {
            throw new OptionsValidationException(PaymentMethodOptions.SectionName, typeof(PaymentMethodOptions), validationResult.Failures);
        }
        
        services.TryAddSingleton(options!);
        return services;
    }
    
    public static PaymentMethodOptions? GetPaymentMethodOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(PaymentMethodOptions.SectionName);
        if (!section.Exists())
        {
            return null;
        }
        
        PaymentMethodOptions options = new();
        section.Bind(options);
        return options;
    }
}
