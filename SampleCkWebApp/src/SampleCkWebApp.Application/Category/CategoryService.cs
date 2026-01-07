using ErrorOr;
using SampleCkWebApp.Application.Category.Interfaces.Application;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Contracts.DTOs.Category;
using Domain.Entities;
using api.Mappers;
using Domain.Errors;

namespace SampleCkWebApp.Application.Category;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;
    private readonly CategoryValidator _categoryValidator;

    public CategoryService(
        IRepository<Domain.Entities.Category> categoryRepository,
        CategoryValidator categoryValidator)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
    }

    public async Task<ErrorOr<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => c.ToDto()).ToList();
    }

    public async Task<ErrorOr<CategoryDto>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return CategoryErrors.NotFound;

        return category.ToDto();
    }

    public async Task<ErrorOr<CategoryDto>> CreateCategoryAsync(CreateCategoryDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _categoryValidator.ValidateCreateCategory(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var category = request.ToModel();

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return category.ToDto();
    }

    public async Task<ErrorOr<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto request, CancellationToken cancellationToken = default)
    {

        //  Validate input using validator
        var validationResult = _categoryValidator.ValidateUpdateCategory(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return CategoryErrors.NotFound;

        //  Update only provided fields
        if (!string.IsNullOrEmpty(request.Name))
            category.Name = request.Name;

        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return category.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
         var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return CategoryErrors.NotFound;

        await _categoryRepository.DeleteAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return Result.Success;
    }
}
