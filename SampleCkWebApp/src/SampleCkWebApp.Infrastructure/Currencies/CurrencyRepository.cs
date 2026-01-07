using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Infrastructure.Currencies.Repositories;

public class CurrencyRepository : IRepository<Currency>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Currency> _currencySet;

    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
        _currencySet = context.Set<Currency>();
    }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        return await _currencySet.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Currency>> GetAllAsync()
    {
        return await _currencySet.ToListAsync();
    }

    public async Task AddAsync(Currency entity)
    {
        await _currencySet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Currency> entities)
    {
        await _currencySet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(Currency entity)
    {
        _currencySet.Update(entity);
    }

    public async Task DeleteAsync(Currency entity)
    {
        _currencySet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<Currency> entities)
    {
        _currencySet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}