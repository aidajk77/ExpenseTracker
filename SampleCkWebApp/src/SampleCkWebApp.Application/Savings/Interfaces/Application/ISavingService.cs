using ErrorOr;
using Contracts.DTOs.Saving;

namespace SampleCkWebApp.Application.Savings.Interfaces.Application;

public interface ISavingService
{
    Task<ErrorOr<IEnumerable<SavingDto>>> GetAllSavingsAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<SavingDto>> GetSavingByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<SavingDto>> CreateSavingAsync(CreateSavingDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<SavingDto>> UpdateSavingAsync(int id, UpdateSavingDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeleteSavingAsync(int id, CancellationToken cancellationToken = default);
}
