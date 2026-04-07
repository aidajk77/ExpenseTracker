using ErrorOr;
using Contracts.DTOs.Category;
using Domain.Enums;

namespace SampleCkWebApp.Application.Category.Interfaces.Application;

public interface ICategoryService
{
    Task<ErrorOr<IEnumerable<CategoryDto>>> GetAllUserCategoriesAsync(int userId, CancellationToken cancellationToken = default);
    Task<ErrorOr<CategoryDto>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<CategoryDto>> CreateCategoryAsync(CreateCategoryDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<CategoryDto>> UpdateCategoryAsync(int id, TransactionType type, decimal amountChange, CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeleteCategoryAsync(int id, CancellationToken cancellationToken = default);
}
