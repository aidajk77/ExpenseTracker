using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;

namespace SampleCkWebApp.Infrastructure.Users.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<User> _userSet;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
        _userSet = context.Set<User>();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userSet.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userSet.ToListAsync();
    }

    public async Task AddAsync(User entity)
    {
        await _userSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<User> entities)
    {
        await _userSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(User entity)
    {
        _userSet.Update(entity);
    }

    public async Task DeleteAsync(User entity)
    {
        _userSet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<User> entities)
    {
        _userSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
