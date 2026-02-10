using ErrorOr;

namespace Domain.Errors;

/// <summary>
/// Budget-related error messages.
/// </summary>
public static class BudgetErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(BudgetErrors)}.{nameof(NotFound)}", "Budget not found.");

    public static Error CurrentAmountExceedsLimit =>
        Error.Validation("Budget.CurrentAmountExceedsLimit", "Current amount cannot exceed the budget limit");

    public static Error InvalidId =>
        Error.Validation($"{nameof(BudgetErrors)}.{nameof(InvalidId)}", "Invalid budget ID.");

    public static Error InvalidCategory =>
        Error.Validation($"{nameof(BudgetErrors)}.{nameof(InvalidCategory)}", "Category ID must be greater than 0.");

    public static Error InvalidAmountLimit =>
        Error.Validation($"{nameof(BudgetErrors)}.{nameof(InvalidAmountLimit)}", "Amount limit must be greater than 0.");

    public static Error InvalidMonth =>
        Error.Validation($"{nameof(BudgetErrors)}.{nameof(InvalidMonth)}", "Month must be between 1 and 12.");

    public static Error InvalidYear =>
        Error.Validation($"{nameof(BudgetErrors)}.{nameof(InvalidYear)}", "Year must be between 2000 and 3000.");

    public static Error DuplicateBudget =>
        Error.Conflict($"{nameof(BudgetErrors)}.{nameof(DuplicateBudget)}", "Budget for this category already exists for the specified month and year.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(BudgetErrors)}.{nameof(CreateFailed)}", "Failed to create budget.");
    
    public static Error UpdateFailed =>
        Error.Failure($"{nameof(BudgetErrors)}.{nameof(UpdateFailed)}", "Failed to update budget.");

    public static Error DeleteFailed =>
        Error.Failure($"{nameof(BudgetErrors)}.{nameof(DeleteFailed)}", "Failed to delete budget.");

    public static Error CategoryNotFound =>
        Error.NotFound($"{nameof(BudgetErrors)}.{nameof(CategoryNotFound)}", "Category not found.");
}


