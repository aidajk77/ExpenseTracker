using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;

namespace SampleCkWebApp.Infrastructure.Transactions.Repositories;

public class TransactionRepository : IRepository<Transaction>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Transaction> _transactionSet;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
        _transactionSet = context.Set<Transaction>();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _transactionSet.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _transactionSet.ToListAsync();
    }

    public async Task AddAsync(Transaction entity)
    {
        await _transactionSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Transaction> entities)
    {
        await _transactionSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(Transaction entity)
    {
        _transactionSet.Update(entity);
    }

    public async Task DeleteAsync(Transaction entity)
    {
        _transactionSet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<Transaction> entities)
    {
        _transactionSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
