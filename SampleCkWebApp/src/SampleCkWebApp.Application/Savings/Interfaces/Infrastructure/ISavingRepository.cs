using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Savings.Interfaces.Infrastructure;

public interface ISavingRepository : IRepository<Saving>
{
    Task<IEnumerable<Saving>> GetUserSavingsAsync(int userId);

    Task<IEnumerable<Saving>> GetUserNonCompletedSavingsAsync(int userId);

    Task<IEnumerable<int>> GetExistingUserIdsAsync(IEnumerable<int> userIds);
}