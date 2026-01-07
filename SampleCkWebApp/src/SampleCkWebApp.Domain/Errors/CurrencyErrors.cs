using ErrorOr;

namespace Domain.Errors;

/// <summary>
/// Currency-related error messages.
/// </summary>
public static class CurrencyErrors
{
    public static Error NotFound =>
        Error.NotFound($"{nameof(CurrencyErrors)}.{nameof(NotFound)}", "Currency not found.");

    public static Error InvalidId =>
        Error.Validation($"{nameof(CurrencyErrors)}.{nameof(InvalidId)}", "Invalid currency ID.");

    public static Error CodeRequired =>
        Error.Validation($"{nameof(CurrencyErrors)}.{nameof(CodeRequired)}", "Currency code is required.");

    public static Error InvalidCode =>
        Error.Validation($"{nameof(CurrencyErrors)}.{nameof(InvalidCode)}", "Currency code must be exactly 3 letters (e.g., USD, EUR, GBP).");

    public static Error NameRequired =>
        Error.Validation($"{nameof(CurrencyErrors)}.{nameof(NameRequired)}", "Currency name is required.");

    public static Error InvalidName =>
        Error.Validation($"{nameof(CurrencyErrors)}.{nameof(InvalidName)}", "Currency name cannot exceed 50 characters.");

    public static Error SymbolRequired =>
        Error.Validation($"{nameof(CurrencyErrors)}.{nameof(SymbolRequired)}", "Currency symbol is required.");

    public static Error InvalidSymbol =>
        Error.Validation($"{nameof(CurrencyErrors)}.{nameof(InvalidSymbol)}", "Currency symbol cannot exceed 5 characters.");
    
    public static Error DuplicateCode =>
        Error.Conflict($"{nameof(CurrencyErrors)}.{nameof(DuplicateCode)}", "Currency with this code already exists.");

    public static Error CreateFailed =>
        Error.Failure($"{nameof(CurrencyErrors)}.{nameof(CreateFailed)}", "Failed to create currency.");

    public static Error UpdateFailed =>
        Error.Failure($"{nameof(CurrencyErrors)}.{nameof(UpdateFailed)}", "Failed to update currency.");

    public static Error DeleteFailed =>
        Error.Failure($"{nameof(CurrencyErrors)}.{nameof(DeleteFailed)}", "Failed to delete currency.");

}
