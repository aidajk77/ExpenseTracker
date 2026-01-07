using ErrorOr;
using Contracts.DTOs.UserSaving;

namespace SampleCkWebApp.Application.UserSaving.Interfaces.Application;

public interface IUserSavingService
{
    Task<ErrorOr<IEnumerable<UserSavingDto>>> GetAllUserSavingsAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<UserSavingDto>> GetUserSavingByIdAsync(int userId, int savingId, CancellationToken cancellationToken = default);
    Task<ErrorOr<UserSavingDto>> CreateUserSavingAsync(CreateUserSavingDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<UserSavingDto>> UpdateUserSavingAsync(int userId, int savingId, UpdateUserSavingDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeleteUserSavingAsync(int userId, int savingId, CancellationToken cancellationToken = default);
}
