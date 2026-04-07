using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Transactions.Options;
using SampleCkWebApp.Infrastructure.Transactions.Repositories;
using SampleCkWebApp.Application.Transaction.Interfaces.Infrastructure;

namespace SampleCkWebApp.Infrastructure.Transactions;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var transactionOptions = configuration.GetTransactionOptions();
        services.TryAddTransactionOptions(transactionOptions);
        
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        
        return services;
    }
}
