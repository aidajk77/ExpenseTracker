using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Budgets;
using SampleCkWebApp.Application.Category;
using SampleCkWebApp.Application.Currencies;
using SampleCkWebApp.Application.PaymentMethod;
using SampleCkWebApp.Application.Savings;
using SampleCkWebApp.Application.Transaction;
using SampleCkWebApp.Application.UserSaving;
using SampleCkWebApp.Application.Users;
using SampleCkWebApp.Infrastructure.Budgets;
using SampleCkWebApp.Infrastructure.Categories;
using SampleCkWebApp.Infrastructure.Currencies;
using SampleCkWebApp.Infrastructure.PaymentMethods;
using SampleCkWebApp.Infrastructure.Savings;
using SampleCkWebApp.Infrastructure.Transactions;
using SampleCkWebApp.Infrastructure.UserSavings;
using SampleCkWebApp.Infrastructure.Users;
using SampleCkWebApp.Infrastructure.Budgets.Options;
using SampleCkWebApp.Infrastructure.Categories.Options;
using SampleCkWebApp.Infrastructure.Currencies.Options;
using SampleCkWebApp.Infrastructure.PaymentMethods.Options;
using SampleCkWebApp.Infrastructure.Savings.Options;
using SampleCkWebApp.Infrastructure.Transactions.Options;
using SampleCkWebApp.Infrastructure.UserSavings.Options;
using SampleCkWebApp.Infrastructure.Users.Options;
using SampleCkWebApp.WebApi.Options;
using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Infrastructure.Data;

namespace SampleCkWebApp.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration["Database:ConnectionString"]));

        // ========== OPTIONS CONFIGURATION ==========
        //  Users Options
        services.TryAddUserOptions(configuration.GetUserOptions());
        
        //  User Savings Options
        services.TryAddUserSavingOptions(configuration.GetUserSavingOptions());
        
        //  Budget Options
        services.TryAddBudgetOptions(configuration.GetBudgetOptions());
        
        //  Categories Options
        services.TryAddCategoryOptions(configuration.GetCategoryOptions());
        
        //  Currencies Options
        services.TryAddCurrencyOptions(configuration.GetCurrencyOptions());
        
        //  Payment Methods Options
        services.TryAddPaymentMethodOptions(configuration.GetPaymentMethodOptions());
        
        //  Savings Options
        services.TryAddSavingOptions(configuration.GetSavingOptions());
        
        //  Transactions Options
        services.TryAddTransactionOptions(configuration.GetTransactionOptions());


        return services
            .AddUsersApplication()                      //  Users & Auth
            .AddUserSavingApplication()                //   User Savings (junction table)
            .AddBudgetApplication()                     //  Budgets
            .AddCategoryApplication()                 //  Categories
            .AddCurrencyApplication()                 //  Currencies
            .AddPaymentMethodApplication()             //  Payment Methods
            .AddSavingApplication()                    //  Savings
            .AddTransactionApplication();    
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddUsersInfrastructure(configuration)       //  Users Infrastructure
            .AddUserSavingsInfrastructure(configuration) //  User Savings Infrastructure
            .AddBudgetInfrastructure(configuration)      //  Budget Infrastructure
            .AddCategoriesInfrastructure(configuration)  //  Categories Infrastructure
            .AddCurrenciesInfrastructure(configuration)  //  Currencies Infrastructure
            .AddPaymentMethodsInfrastructure(configuration) //  Payment Methods Infrastructure
            .AddSavingsInfrastructure(configuration)     //  Savings Infrastructure
            .AddTransactionsInfrastructure(configuration);      //  Transactions Infrastructure
    }
}