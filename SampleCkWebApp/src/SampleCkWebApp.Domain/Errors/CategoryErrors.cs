using ErrorOr;

namespace Domain.Errors;

/// <summary>
/// Category-related error messages.
/// </summary>
public static class CategoryErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(CategoryErrors)}.{nameof(NotFound)}", "Category not found.");

    public static Error InvalidId =>
        Error.Validation($"{nameof(CategoryErrors)}.{nameof(InvalidId)}", "Invalid category ID.");

    public static Error NameRequired =>
        Error.Validation($"{nameof(CategoryErrors)}.{nameof(NameRequired)}", "Category name is required.");

    public static Error InvalidName =>
        Error.Validation($"{nameof(CategoryErrors)}.{nameof(InvalidName)}", "Category name must be between 1 and 100 characters.");

    public static Error DuplicateName =>
        Error.Conflict($"{nameof(CategoryErrors)}.{nameof(DuplicateName)}", "Category with this name already exists.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(CategoryErrors)}.{nameof(CreateFailed)}", "Failed to create category.");

    public static Error UpdateFailed =>
        Error.Failure($"{nameof(CategoryErrors)}.{nameof(UpdateFailed)}", "Failed to update category.");

    public static Error DeleteFailed =>
        Error.Failure($"{nameof(CategoryErrors)}.{nameof(DeleteFailed)}", "Failed to delete category.");
}
