using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;

namespace SampleCkWebApp.Infrastructure.Categories.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Category> _categorySet;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
        _categorySet = context.Set<Category>();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _categorySet.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categorySet.ToListAsync();
    }

    public async Task AddAsync(Category entity)
    {
        await _categorySet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Category> entities)
    {
        await _categorySet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(Category entity)
    {
        _categorySet.Update(entity);
    }

    public async Task DeleteAsync(Category entity)
    {
        _categorySet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<Category> entities)
    {
        _categorySet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
