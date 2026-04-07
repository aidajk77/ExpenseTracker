using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.UserSaving.Interfaces.Infrastructure;

public interface IUserSavingRepository : IRepository<Domain.Entities.UserSaving>
{
    Task<Domain.Entities.UserSaving?> GetUserSavingAsync(int userId, int savingId);

    Task<IEnumerable<Domain.Entities.UserSaving>> GetUserSavingsByUserIdAsync(int userId);

    Task<IEnumerable<Domain.Entities.UserSaving>> GetUserSavingsBySavingIdAsync(int savingId);

    Task<bool> UserExistsInSavingAsync(int userId, int savingId);
}