using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;

namespace SampleCkWebApp.Infrastructure.Savings.Repositories;

public class SavingRepository : IRepository<Saving>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Saving> _savingSet;

    public SavingRepository(ApplicationDbContext context)
    {
        _context = context;
        _savingSet = context.Set<Saving>();
    }

    public async Task<Saving?> GetByIdAsync(int id)
    {
        return await _savingSet.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Saving>> GetAllAsync()
    {
        return await _savingSet.ToListAsync();
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
