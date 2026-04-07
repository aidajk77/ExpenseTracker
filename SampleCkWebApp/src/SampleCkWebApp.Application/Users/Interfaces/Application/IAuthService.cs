using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.User;
using ErrorOr;
using SampleCkWebApp.Contracts.DTOs.User;

namespace SampleCkWebApp.Application.Users.Interfaces.Application
{
    public interface IAuthService
    {
        Task<ErrorOr<AuthResponseDto>> RegisterAsync(RegisterUserDto request, CancellationToken cancellationToken = default);
        Task<ErrorOr<AuthResponseDto>> LoginAsync(LoginUserDto request, CancellationToken cancellationToken = default);
        Task<ErrorOr<Success>> ChangePasswordAsync(int userId, ChangePasswordDto request, CancellationToken cancellationToken = default);
        //Task<ErrorOr<AuthResponseDto>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}