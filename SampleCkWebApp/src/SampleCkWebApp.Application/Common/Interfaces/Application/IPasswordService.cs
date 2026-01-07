using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace SampleCkWebApp.Application.Common.Interfaces.Application
{
    public interface IPasswordService
    {
        public string HashPassword(string password);
        
        public bool VerifyPassword(string password, string hash);

        public string GenerateJwtToken(User user);
    }
}