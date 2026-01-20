using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleCkWebApp.Application.Common.Interfaces.Application;

namespace SampleCkWebApp.Application.Common
{
    public class PasswordService : IPasswordService
    {
        private readonly IConfiguration _configuration;

        public PasswordService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSecret = _configuration["Jwt:Secret"];
            var jwtExpiryMinutes = int.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}