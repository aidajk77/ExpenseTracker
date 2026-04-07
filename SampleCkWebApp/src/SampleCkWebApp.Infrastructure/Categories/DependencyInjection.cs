using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Categories.Options;
using SampleCkWebApp.Infrastructure.Categories.Repositories;
using SampleCkWebApp.Application.Category.Interfaces.Infrastructure;

namespace SampleCkWebApp.Infrastructure.Categories;

public static class DependencyInjection
{
    public static IServiceCollection AddCategoriesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var categoryOptions = configuration.GetCategoryOptions();
        services.TryAddCategoryOptions(categoryOptions);
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        return services;
    }
}
