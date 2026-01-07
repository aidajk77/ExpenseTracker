using ErrorOr;

namespace Domain.Errors;

/// <summary>
/// PaymentMethod-related error messages.
/// </summary>
public static class PaymentMethodErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(PaymentMethodErrors)}.{nameof(NotFound)}", "Payment method not found.");

    public static Error InvalidId =>
        Error.Validation($"{nameof(PaymentMethodErrors)}.{nameof(InvalidId)}", "Invalid payment method ID.");

    public static Error NameRequired =>
        Error.Validation($"{nameof(PaymentMethodErrors)}.{nameof(NameRequired)}", "Payment method name is required.");

    public static Error InvalidName =>
        Error.Validation($"{nameof(PaymentMethodErrors)}.{nameof(InvalidName)}", "Payment method name must be between 1 and 50 characters.");

    public static Error InvalidDescription =>
        Error.Validation($"{nameof(PaymentMethodErrors)}.{nameof(InvalidDescription)}", "Description cannot exceed 255 characters.");

    public static Error DuplicateName =>
        Error.Conflict($"{nameof(PaymentMethodErrors)}.{nameof(DuplicateName)}", "Payment method with this name already exists.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(PaymentMethodErrors)}.{nameof(CreateFailed)}", "Failed to create payment method.");

    public static Error UpdateFailed =>
        Error.Failure($"{nameof(PaymentMethodErrors)}.{nameof(UpdateFailed)}", "Failed to update payment method.");

    public static Error DeleteFailed =>
        Error.Failure($"{nameof(PaymentMethodErrors)}.{nameof(DeleteFailed)}", "Failed to delete payment method.");
}
