using ErrorOr;


namespace Domain.Errors;

/// <summary>
/// Error codes and messages for user-related operations.
/// </summary>
public static class UserErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(UserErrors)}.{nameof(NotFound)}", "User not found.");

    public static Error InvalidCredentials =>
        Error.Unauthorized($"{nameof(UserErrors)}.{nameof(InvalidCredentials)}", "Invalid email or password.");

    public static Error DuplicateEmail =>
        Error.Conflict($"{nameof(UserErrors)}.{nameof(DuplicateEmail)}", "Email already exists.");

    public static Error InvalidPassword =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidPassword)}", "Password must be at least 8 characters long.");
    
    public static Error EmailRequired =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(EmailRequired)}", "Email is required.");

    public static Error PasswordMismatch =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(PasswordMismatch)}", "Passwords do not match.");

    public static Error NameRequired =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(NameRequired)}", "Name is required.");

    public static Error PasswordRequired =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(PasswordRequired)}", "Password is required.");

    public static Error InvalidEmail =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidEmail)}", "The provided email is invalid.");

    public static Error InvalidName =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidName)}", "Name must be between 1 and 100 characters, and there need to be both first and last name.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(UserErrors)}.{nameof(CreateFailed)}", "Failed to create user.");
    
    public static Error InvalidCurrency =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidCurrency)}", "Currency not found.");

    public static Error UpdateFailed =>
        Error.Failure($"{nameof(UserErrors)}.{nameof(UpdateFailed)}", "Failed to update user.");

    public static Error DeleteFailed =>
        Error.Failure($"{nameof(UserErrors)}.{nameof(DeleteFailed)}", "Failed to delete user.");

    public static Error InvalidId =>
        Error.Validation($"{nameof(UserErrors)}.{nameof(InvalidId)}", "Invalid user ID.");
}
