using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using Contracts.DTOs.User;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Application;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Currencies.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Users.Interfaces.Application;
using SampleCkWebApp.Application.Users.Interfaces.Infrastructure;
using SampleCkWebApp.Contracts.DTOs.User;

namespace SampleCkWebApp.Application.Users
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPasswordService _passwordService;
        private readonly AuthValidator _authValidator;

        public AuthService(
            IUserRepository userRepository,
            ICurrencyRepository currencyRepository,
            IPasswordService passwordService,
            AuthValidator authValidator)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _passwordService = passwordService;
            _authValidator = authValidator;
        }        
        
        public async Task<ErrorOr<AuthResponseDto>> RegisterAsync(RegisterUserDto request, CancellationToken cancellationToken = default)
        {

            // Validate input using validator
            var validationResult = _authValidator.ValidateRegistration(request);
            if (validationResult.IsError)
                return validationResult.Errors;

            // Check if email already exists
            var emailExists = await _userRepository.EmailExistsAsync(request.Email);
            if (emailExists)
                return UserErrors.DuplicateEmail;

            //  Validate currency exists
            var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
            if (currency is null)
                return Error.NotFound(description: "Currency not found.");

            // Hash the password
            var passwordHash = _passwordService.HashPassword(request.Password);

            var user = request.ToModel(passwordHash);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = _passwordService.GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                User = user.ToDto()
            };
        }

        public async Task<ErrorOr<AuthResponseDto>> LoginAsync(LoginUserDto request, CancellationToken cancellationToken = default)
        {

            //  Validate input using validator
            var validationResult = _authValidator.ValidateLogin(request);
            if (validationResult.IsError)
                return validationResult.Errors;

            // Find user by email
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            // Check if user exists and password is correct
            if (user is null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
                return UserErrors.InvalidCredentials;

            // Generate JWT token
            var token = _passwordService.GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                User = user.ToDto()
            };
        }

        public async Task<ErrorOr<Success>> ChangePasswordAsync(int userId, ChangePasswordDto request, CancellationToken cancellationToken = default)
        {
            // Validate input
            var validationResult = _authValidator.ValidateChangePassword(request);
            if (validationResult.IsError)
                return validationResult.Errors;

            // Get user by ID
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                return UserErrors.NotFound;

            // Verify current password
            if (!_passwordService.VerifyPassword(request.CurrentPassword, user.PasswordHash))
                return UserErrors.InvalidCredentials;

            // Hash new password
            var newPasswordHash = _passwordService.HashPassword(request.NewPassword);

            // Update password
            user.PasswordHash = newPasswordHash;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return Result.Success;
        }
    }
}