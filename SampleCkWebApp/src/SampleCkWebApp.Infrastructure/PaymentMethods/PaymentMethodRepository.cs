using Microsoft.EntityFrameworkCore;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;

namespace SampleCkWebApp.Infrastructure.PaymentMethods.Repositories;

public class PaymentMethodRepository : IRepository<PaymentMethod>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<PaymentMethod> _paymentMethodSet;

    public PaymentMethodRepository(ApplicationDbContext context)
    {
        _context = context;
        _paymentMethodSet = context.Set<PaymentMethod>();
    }

    public async Task<PaymentMethod?> GetByIdAsync(int id)
    {
        return await _paymentMethodSet.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
    {
        return await _paymentMethodSet.ToListAsync();
    }

    public async Task AddAsync(PaymentMethod entity)
    {
        await _paymentMethodSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<PaymentMethod> entities)
    {
        await _paymentMethodSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(PaymentMethod entity)
    {
        _paymentMethodSet.Update(entity);
    }

    public async Task DeleteAsync(PaymentMethod entity)
    {
        _paymentMethodSet.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<PaymentMethod> entities)
    {
        _paymentMethodSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
