using ErrorOr;


namespace Domain.Errors;

/// <summary>
/// Error codes and messages for saving-related operations.
/// </summary>
public static class SavingErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(SavingErrors)}.{nameof(NotFound)}", "Saving not found.");

    public static Error InvalidAmount =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidAmount)}", "Amount must be greater than 0.");

    public static Error InvalidStatus =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidStatus)}", "Invalid saving status.");

    public static Error NameRequired =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(NameRequired)}", "Saving name is required.");

    public static Error TargetAmountRequired =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(TargetAmountRequired)}", "Target amount is required.");

    public static Error InvalidTargetAmount =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidTargetAmount)}", "Target amount must be greater than 0.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(SavingErrors)}.{nameof(CreateFailed)}", "Failed to create saving.");

    public static Error UpdateFailed =>
        Error.Failure($"{nameof(SavingErrors)}.{nameof(UpdateFailed)}", "Failed to update saving.");

    public static Error DeleteFailed =>
        Error.Failure($"{nameof(SavingErrors)}.{nameof(DeleteFailed)}", "Failed to delete saving.");
    
    public static Error InvalidId =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidId)}", "Invalid saving ID.");

    public static Error CannotModifyCompleted =>
        Error.Conflict($"{nameof(SavingErrors)}.{nameof(CannotModifyCompleted)}", "Cannot modify a completed saving.");

    public static Error InvalidTransactionType =>
        Error.Conflict($"{nameof(SavingErrors)}.{nameof(InvalidTransactionType)}", "Only SAVING type transactions can be linked to savings.");
        
    public static Error UserNotPartOfSaving =>
        Error.Conflict($"{nameof(SavingErrors)}.{nameof(UserNotPartOfSaving)}", "User is not part of this saving goal.");

    public static Error InvalidName =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidName)}", "Saving name must be between 1 and 100 characters.");

    public static Error InvalidDescription =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidDescription)}", "Description cannot exceed 500 characters.");

    public static Error InvalidTargetDate =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidTargetDate)}", "Target date must be in the future.");

    public static Error InvalidUserIds =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidUserIds)}", "At least one valid user ID is required.");

    public static Error InvalidUsersEmpty =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidUsersEmpty)}", "At least one user is required.");

    public static Error InvalidUserIdValue =>
        Error.Validation($"{nameof(SavingErrors)}.{nameof(InvalidUserIdValue)}", "All user IDs must be valid.");

    public static Error TargetAmountBelowCurrent =>
        Error.Conflict($"{nameof(SavingErrors)}.{nameof(TargetAmountBelowCurrent)}", "Target amount cannot be less than current amount.");


}
