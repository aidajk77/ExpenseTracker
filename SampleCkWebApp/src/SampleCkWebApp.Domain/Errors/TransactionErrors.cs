using ErrorOr;


namespace Domain.Errors;

/// <summary>
/// Error codes and messages for transaction-related operations.
/// </summary>
public static class TransactionErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(TransactionErrors)}.{nameof(NotFound)}", "Transaction not found.");

    public static Error InvalidAmount =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidAmount)}", "Amount must be greater than 0.");

    public static Error InvalidCategory =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidCategory)}", "Category not found or invalid.");

    public static Error InvalidPaymentMethod =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidPaymentMethod)}", "Payment method not found or invalid.");

    public static Error InvalidUser =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidUser)}", "User not found or invalid.");

    public static Error DescriptionRequired =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(DescriptionRequired)}", "Transaction description is required.");

    public static Error InvalidDate =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidDate)}", "Transaction date is invalid.");

    public static Error InvalidSavingId =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidSavingId)}", "Invalid saving ID.");

    public static Error InvalidType =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidType)}", "Invalid transaction type.");

    public static Error DescriptionTooLong =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(DescriptionTooLong)}", "Description cannot exceed 255 characters.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(TransactionErrors)}.{nameof(CreateFailed)}", "Failed to create transaction.");

    public static Error UpdateFailed =>
        Error.Failure($"{nameof(TransactionErrors)}.{nameof(UpdateFailed)}", "Failed to update transaction.");
    
    public static Error DeleteFailed =>
        Error.Failure($"{nameof(TransactionErrors)}.{nameof(DeleteFailed)}", "Failed to delete transaction.");

    public static Error InvalidId =>
        Error.Validation($"{nameof(TransactionErrors)}.{nameof(InvalidId)}", "Invalid transaction ID.");

    public static Error CannotModify =>
        Error.Conflict($"{nameof(TransactionErrors)}.{nameof(CannotModify)}", "Cannot modify this transaction.");
}
