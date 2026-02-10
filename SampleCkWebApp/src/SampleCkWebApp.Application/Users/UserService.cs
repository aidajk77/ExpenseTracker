using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Users.Interfaces.Application;
using Contracts.DTOs.User;
using Domain.Entities;
using api.Mappers;
using Domain.Errors;

namespace SampleCkWebApp.Application.Users;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Currency> _currencyRepository;

    public UserService(IRepository<User> userRepository, IRepository<Currency> currencyRepository)
    {
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
    }

    public async Task<ErrorOr<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => u.ToDto()).ToList();
    }

    public async Task<ErrorOr<UserDto>> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return UserErrors.NotFound;

        return user.ToDto();
    }

    public async Task<ErrorOr<UserDto>> UpdateUserAsync(int id, UpdateUserDto request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return UserErrors.NotFound;

        // Apply updates only if provided
        if (request.Username != null)
            user.Username = request.Username;

        if (request.Email != null)
        {
            // Check if new email already exists
            var existingUsers = await _userRepository.GetAllAsync();
            if (existingUsers.Any(u => u.Email == request.Email && u.Id != id))
                return UserErrors.DuplicateEmail;

            user.Email = request.Email;
        }

        if (request.CurrencyId.HasValue)
        {
            // Validate currency exists
            var currencyExists = await _currencyRepository.GetByIdAsync(request.CurrencyId.Value);
            if (currencyExists == null)
                return UserErrors.InvalidCurrency;

            user.CurrencyId = request.CurrencyId.Value;
        }

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return user.ToDto();
    }

    public async Task<ErrorOr<Success>> DeleteUserAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return UserErrors.NotFound;

        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();

        return Result.Success;
    }
}
