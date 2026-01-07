using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.User;
using ErrorOr;
using Domain.Errors;

namespace SampleCkWebApp.Application.Users
{
    public class AuthValidator
    {
        public ErrorOr<Success> ValidateRegistration(RegisterUserDto request)
        {
            var errors = new List<Error>();

            //  Validate Name (full name)
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add(UserErrors.NameRequired);
            else if (request.Name.Length > 100)
                errors.Add(UserErrors.InvalidName);
            else if (request.Name.Split(' ').Length < 2)
                errors.Add(UserErrors.InvalidName);

            //  Validate Email
            if (string.IsNullOrWhiteSpace(request.Email))
                errors.Add(UserErrors.EmailRequired);
            else if (!IsValidEmail(request.Email))
                errors.Add(UserErrors.InvalidEmail);

            //  Validate Password
            if (string.IsNullOrWhiteSpace(request.Password))
                errors.Add(UserErrors.PasswordRequired);
            else if (request.Password.Length < 8)
                errors.Add(UserErrors.InvalidPassword);

            //  Validate CurrencyId
            if (request.CurrencyId <= 0)
                errors.Add(UserErrors.InvalidCurrency);
            

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateLogin(LoginUserDto request)
        {
            var errors = new List<Error>();

            //  Validate Email
            if (string.IsNullOrWhiteSpace(request.Email))
                errors.Add(UserErrors.EmailRequired);
            else if (!IsValidEmail(request.Email))
                errors.Add(UserErrors.InvalidEmail);

            //  Validate Password
            if (string.IsNullOrWhiteSpace(request.Password))
                errors.Add(UserErrors.PasswordRequired);
            else if (request.Password.Length < 8)
                errors.Add(UserErrors.InvalidPassword);

            return errors.Count > 0 ? errors : Result.Success;
        }
        

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}