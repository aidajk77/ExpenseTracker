using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.PaymentMethods.Options;
using SampleCkWebApp.Infrastructure.PaymentMethods.Repositories;
using SampleCkWebApp.Application.PaymentMethod.Interfaces.Infrastructure;

namespace SampleCkWebApp.Infrastructure.PaymentMethods;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentMethodsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var paymentMethodOptions = configuration.GetPaymentMethodOptions();
        services.TryAddPaymentMethodOptions(paymentMethodOptions);
        
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        
        return services;
    }
}
