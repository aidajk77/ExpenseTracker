using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;
using SampleCkWebApp.Application.UserSaving.Interfaces.Infrastructure;

namespace SampleCkWebApp.Infrastructure.UserSavings.Repositories;

public class UserSavingRepository : IUserSavingRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<UserSaving> _userSavingSet;

    public UserSavingRepository(ApplicationDbContext context)
    {
        _context = context;
        _userSavingSet = context.Set<UserSaving>();
    }

    public async Task<UserSaving?> GetUserSavingAsync(int userId, int savingId)
    {
        return await _userSavingSet
            .AsNoTracking()
            .Where(us => us.UserId == userId && us.SavingId == savingId)
            .Include(us => us.User)
            .Include(us => us.Saving)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserSaving>> GetUserSavingsByUserIdAsync(int userId)
    {
        return await _userSavingSet
            .AsNoTracking()
            .Where(us => us.UserId == userId)
            .Include(us => us.User)
            .Include(us => us.Saving)
            .OrderByDescending(us => us.JoinedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserSaving>> GetUserSavingsBySavingIdAsync(int savingId)
    {
        return await _userSavingSet
            .AsNoTracking()
            .Where(us => us.SavingId == savingId)
            .Include(us => us.User)
            .Include(us => us.Saving)
            .OrderBy(us => us.User.Username)
            .ToListAsync();
    }

    public async Task<bool> UserExistsInSavingAsync(int userId, int savingId)
    {
        return await _userSavingSet
            .AsNoTracking()
            .AnyAsync(us => us.UserId == userId && us.SavingId == savingId);
    }
    public async Task<UserSaving?> GetByIdAsync(int id)
    {
        return await _userSavingSet
            .Include(us => us.User)      
            .Include(us => us.Saving)    
            .FirstOrDefaultAsync(us => us.UserId == id);
    }

    public async Task<IEnumerable<UserSaving>> GetAllAsync()
    {
        return await _userSavingSet
            .Include(us => us.User)      
            .Include(us => us.Saving)    
            .ToListAsync();
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
