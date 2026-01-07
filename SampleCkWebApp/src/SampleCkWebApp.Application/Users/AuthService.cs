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
using SampleCkWebApp.Application.Users.Interfaces.Application;
using SampleCkWebApp.Contracts.DTOs.User;

namespace SampleCkWebApp.Application.Users
{
    public class AuthService : IAuthService
    {

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IPasswordService _passwordService;
        private readonly AuthValidator _authValidator;

        public AuthService(
            IRepository<User> userRepository,
            IRepository<Currency> currencyRepository,
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
            var users = await _userRepository.GetAllAsync();
            if (users.Any(u => u.Email == request.Email))
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
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == request.Email);

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
    }
}