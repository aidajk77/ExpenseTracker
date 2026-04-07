using ErrorOr;
using Contracts.DTOs.Saving;

namespace SampleCkWebApp.Application.Savings.Interfaces.Application;

public interface ISavingService
{
    Task<ErrorOr<IEnumerable<SavingDto>>> GetAllSavingsAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<SavingDto>>> GetUserSavingsAsync(
        int userId, 
        CancellationToken cancellationToken = default);
    Task<ErrorOr<IEnumerable<SavingDto>>> GetUserNonCompletedSavingsAsync(
        int userId, 
        CancellationToken cancellationToken = default);
    Task<ErrorOr<SavingDto>> GetSavingByIdAsync(int id, int userId, CancellationToken cancellationToken = default);
    Task<ErrorOr<SavingDto>> CreateSavingAsync(CreateSavingDto request, int userId, CancellationToken cancellationToken = default);
    Task<ErrorOr<SavingDto>> UpdateSavingAsync(int id, UpdateSavingDto request, int userId, CancellationToken cancellationToken = default);
    Task<ErrorOr<SavingDto>> AddSavingTransactionAsync(
        int savingId,
        int userId,
        decimal amount,
        CancellationToken cancellationToken = default);
    
    Task<ErrorOr<Success>> RemoveSavingTransactionAsync(
        int savingId,
        int userId,
        decimal amount,
        CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeleteSavingAsync(int id, int userId, CancellationToken cancellationToken = default);
}
