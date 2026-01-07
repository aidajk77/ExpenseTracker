using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.PaymentMethods;
using SampleCkWebApp.Application.PaymentMethods.Interfaces.Application;

namespace SampleCkWebApp.Application.PaymentMethod;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentMethodApplication(this IServiceCollection services)
    {
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();

        services.AddScoped<PaymentMethodValidator>();
        
        return services;
    }
}
