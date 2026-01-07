using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;

namespace SampleCkWebApp.Infrastructure.UserSavings.Repositories;

public class UserSavingRepository : IRepository<UserSaving>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<UserSaving> _userSavingSet;

    public UserSavingRepository(ApplicationDbContext context)
    {
        _context = context;
        _userSavingSet = context.Set<UserSaving>();
    }

    public async Task<UserSaving?> GetByIdAsync(int id)
    {
        return await _userSavingSet.FirstOrDefaultAsync(us => us.UserId == id);
    }

    public async Task<IEnumerable<UserSaving>> GetAllAsync()
    {
        return await _userSavingSet.ToListAsync();
    }

    public async Task AddAsync(UserSaving entity)
    {
        await _userSavingSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<UserSaving> entities)
    {
        await _userSavingSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(UserSaving entity)
    {
        _userSavingSet.Update(entity);
    }

    public async Task DeleteAsync(UserSaving entity)
    {
        _userSavingSet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<UserSaving> entities)
    {
        _userSavingSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
