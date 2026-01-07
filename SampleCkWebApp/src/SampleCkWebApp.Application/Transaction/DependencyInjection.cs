using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Transaction.Interfaces.Application;

namespace SampleCkWebApp.Application.Transaction;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionApplication(this IServiceCollection services)
    {
        services.AddScoped<ITransactionService, TransactionService>();

        services.AddScoped<TransactionValidator>();
        
        return services;
    }
}
