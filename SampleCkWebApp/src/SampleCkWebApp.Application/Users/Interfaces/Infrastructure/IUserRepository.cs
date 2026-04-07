using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Users.Interfaces.Infrastructure;

public interface IUserRepository : IRepository<User>
{
    Task<bool> EmailExistsAsync(string email);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> EmailExistsForOtherUserAsync(string email, int userId);
}