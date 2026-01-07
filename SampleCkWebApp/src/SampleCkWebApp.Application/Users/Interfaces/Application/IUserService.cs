using ErrorOr;
using Contracts.DTOs.User;

namespace SampleCkWebApp.Application.Users.Interfaces.Application;

public interface IUserService
{
    Task<ErrorOr<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<UserDto>> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<UserDto>> UpdateUserAsync(int id, UpdateUserDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeleteUserAsync(int id, CancellationToken cancellationToken = default);
}
