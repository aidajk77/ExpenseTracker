using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;
using Domain.Enums;
using SampleCkWebApp.Application.Savings.Interfaces.Infrastructure;

namespace SampleCkWebApp.Infrastructure.Savings.Repositories;

public class SavingRepository : ISavingRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Saving> _savingSet;
    private readonly DbSet<User> _userSet;

    public SavingRepository(ApplicationDbContext context)
    {
        _context = context;
        _savingSet = context.Set<Saving>();
        _userSet = context.Set<User>();
    }

    public async Task<IEnumerable<Saving>> GetUserSavingsAsync(int userId)
    {
        return await _savingSet
            .AsNoTracking()
            .Where(s => s.UserSavings.Any(us => us.UserId == userId)) 
            .Include(s => s.UserSavings)
            .ThenInclude(us => us.User)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Saving>> GetUserNonCompletedSavingsAsync(int userId)
    {
        return await _savingSet
            .AsNoTracking()
            .Where(s => 
                s.UserSavings.Any(us => us.UserId == userId) &&  
                s.Status != SavingStatus.Completed)
            .Include(s => s.UserSavings)
            .ThenInclude(us => us.User)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }
    public async Task<IEnumerable<int>> GetExistingUserIdsAsync(IEnumerable<int> userIds)
    {
        return await _userSet
            .AsNoTracking()
            .Where(u => userIds.Contains(u.Id))
            .Select(u => u.Id)
            .ToListAsync();
    }
    public async Task<Saving?> GetByIdAsync(int id)
    {
        return await _savingSet
            .Include(s => s.UserSavings)
            .ThenInclude(us => us.User)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Saving>> GetAllAsync()
    {
        return await _savingSet
            .Include(s => s.UserSavings)
            .ThenInclude(us => us.User)
            .ToListAsync();
    }

    public async Task AddAsync(Saving entity)
    {
        await _savingSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Saving> entities)
    {
        await _savingSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(Saving entity)
    {
        _savingSet.Update(entity);
    }

    public async Task DeleteAsync(Saving entity)
    {
        _savingSet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<Saving> entities)
    {
        _savingSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}