using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Category.Interfaces.Infrastructure;

public interface ICategoryRepository : IRepository<Domain.Entities.Category>
{
    Task<IEnumerable<Domain.Entities.Category>> GetUserCategoriesAsync(int userId);
}