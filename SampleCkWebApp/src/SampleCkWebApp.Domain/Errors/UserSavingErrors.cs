using ErrorOr;

namespace Domain.Errors;

/// <summary>
/// UserSaving-related error messages.
/// </summary>
public static class UserSavingErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(UserSavingErrors)}.{nameof(NotFound)}", "User saving not found.");

    public static Error InvalidUserId =>
        Error.Validation($"{nameof(UserSavingErrors)}.{nameof(InvalidUserId)}", "Invalid user ID.");

    public static Error InvalidSavingId =>
        Error.Validation($"{nameof(UserSavingErrors)}.{nameof(InvalidSavingId)}", "Invalid saving ID.");

    public static Error AmountRequired =>
        Error.Validation($"{nameof(UserSavingErrors)}.{nameof(AmountRequired)}", "Amount is required.");

    public static Error InvalidAmount =>
        Error.Validation($"{nameof(UserSavingErrors)}.{nameof(InvalidAmount)}", "Amount must be greater than zero.");

    public static Error InvalidId =>
        Error.Validation($"{nameof(UserSavingErrors)}.{nameof(InvalidId)}", "Invalid user saving ID.");

    public static Error DeleteFailed =>
        Error.Failure($"{nameof(UserSavingErrors)}.{nameof(DeleteFailed)}", "Failed to delete user saving.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(UserSavingErrors)}.{nameof(CreateFailed)}", "Failed to create user saving.");

    public static Error UpdateFailed =>
        Error.Failure($"{nameof(UserSavingErrors)}.{nameof(UpdateFailed)}", "Failed to update user saving.");

    public static Error UserNotFound =>
        Error.NotFound($"{nameof(UserSavingErrors)}.{nameof(UserNotFound)}", "User not found.");

    public static Error SavingNotFound =>
        Error.NotFound($"{nameof(UserSavingErrors)}.{nameof(SavingNotFound)}", "Saving not found.");
    
    public static Error AlreadyExists =>
        Error.Conflict($"{nameof(UserSavingErrors)}.{nameof(AlreadyExists)}", "User is already part of this saving.");

}
