using Microsoft.Extensions.DependencyInjection;
using SampleCkWebApp.Application.Category.Interfaces.Application;

namespace SampleCkWebApp.Application.Category;

public static class DependencyInjection
{
    public static IServiceCollection AddCategoryApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<CategoryValidator>();
        
        return services;
    }
}
