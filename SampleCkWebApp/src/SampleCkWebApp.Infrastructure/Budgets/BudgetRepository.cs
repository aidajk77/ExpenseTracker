using SampleCkWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;

namespace SampleCkWebApp.Infrastructure.Budgets.Repositories;

public class BudgetRepository : IRepository<Budget>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Budget> _budgetSet;

    public BudgetRepository(ApplicationDbContext context)
    {
        _context = context;
        _budgetSet = context.Set<Budget>();
    }

    public async Task<Budget?> GetByIdAsync(int id)
    {
        return await _budgetSet.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Budget>> GetAllAsync()
    {
        return await _budgetSet.ToListAsync();
    }

    public async Task AddAsync(Budget entity)
    {
        await _budgetSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Budget> entities)
    {
        await _budgetSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(Budget entity)
    {
        _budgetSet.Update(entity);
    }

    public async Task DeleteAsync(Budget entity)
    {
        _budgetSet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<Budget> entities)
    {
        _budgetSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}