using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using SampleCkWebApp.Infrastructure.Data;
using Domain.Enums;
using SampleCkWebApp.Application.Transaction.Interfaces.Infrastructure;

namespace SampleCkWebApp.Infrastructure.Transactions.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Transaction> _transactionSet;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
        _transactionSet = context.Set<Transaction>();
    }

    private DateTime EnsureUtcDateTime(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
        
        return dateTime.Kind == DateTimeKind.Local 
            ? dateTime.ToUniversalTime() 
            : dateTime;
    }

    public async Task<(IEnumerable<Transaction> transactions, int totalCount)> GetUserTransactionsPagedAsync(
        int userId,
        int page,
        int limit,
        TransactionType? type = null,
        int? categoryId = null,
        int? savingId = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var query = _transactionSet
            .AsNoTracking()
            .Where(t => t.UserId == userId); 

        if (type.HasValue)
            query = query.Where(t => t.Type == type.Value);

        if (categoryId.HasValue && categoryId > 0)
            query = query.Where(t => t.CategoryId == categoryId.Value);

        if (savingId.HasValue && savingId > 0)
            query = query.Where(t => t.SavingId == savingId.Value);

        if (startDate.HasValue)
        {
            var utcStartDate = EnsureUtcDateTime(startDate.Value);
            query = query.Where(t => t.Date >= utcStartDate);
        }
        if (endDate.HasValue)
        {
            var utcEndDate = EnsureUtcDateTime(endDate.Value);
            // Include entire day by checking < next day
            query = query.Where(t => t.Date < utcEndDate.AddDays(1));
        }

        var totalCount = await query.CountAsync();

        var transactions = await query
            .OrderByDescending(t => t.Date)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return (transactions, totalCount);
    }

    public async Task<(bool userExists, bool paymentMethodExists, bool categoryExists, bool savingExists, bool userInSaving)> 
    ValidateTransactionDependenciesAsync(
        int userId,
        int paymentMethodId,
        int? categoryId,
        int? savingId)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        var paymentMethodExists = await _context.Set<PaymentMethod>().AnyAsync(pm => pm.Id == paymentMethodId);
        var categoryExists = categoryId.HasValue ? await _context.Set<Category>().AnyAsync(c => c.Id == categoryId.Value) : true;
        var savingExists = savingId.HasValue ? await _context.Set<Saving>().AnyAsync(s => s.Id == savingId.Value) : true;
        var userInSaving = savingId.HasValue ? await _context.Set<UserSaving>().AnyAsync(us => us.UserId == userId && us.SavingId == savingId.Value) : true;

        return (userExists, paymentMethodExists, categoryExists, savingExists, userInSaving);
    }

    public async Task<decimal> GetUserMonthlyTotalAsync(
        int userId,
        TransactionType type,
        int month,
        int year)
    {

        var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        var endDate = startDate.AddMonths(1);

        return await _transactionSet
            .AsNoTracking()
            .Where(t => 
                t.UserId == userId &&
                t.Type == type &&
                t.Date >= startDate &&
                t.Date < endDate) 
            .SumAsync(t => t.Amount);
    }

    public async Task<decimal> GetUserTotalByTypeAndDateRangeAsync(
        int userId,
        TransactionType type,
        DateTime startDate,
        DateTime endDate)
    {
        startDate = EnsureUtcDateTime(startDate);
        endDate = EnsureUtcDateTime(endDate);

        return await _transactionSet
            .AsNoTracking()
            .Where(t =>
                t.UserId == userId &&
                t.Type == type &&
                t.Date >= startDate &&
                t.Date < endDate.AddDays(1))
            .SumAsync(t => t.Amount);
    }

    public async Task<IEnumerable<Transaction>> GetBySavingIdAsync(int savingId)
    {
        return await _transactionSet
            .AsNoTracking()
            .Where(t => t.SavingId == savingId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByCategoryIdAsync(int categoryId)
    {
        return await _transactionSet
            .AsNoTracking()
            .Where(t => t.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _transactionSet
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _transactionSet
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Transaction entity)
    {
        //  Ensure DateTime is UTC before saving
        if (entity.Date.Kind == DateTimeKind.Unspecified)
        {
            entity.Date = DateTime.SpecifyKind(entity.Date, DateTimeKind.Utc);
        }
        if (entity.CreatedAt.Kind == DateTimeKind.Unspecified)
        {
            entity.CreatedAt = DateTime.SpecifyKind(entity.CreatedAt, DateTimeKind.Utc);
        }
        
        await _transactionSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Transaction> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.Date.Kind == DateTimeKind.Unspecified)
            {
                entity.Date = DateTime.SpecifyKind(entity.Date, DateTimeKind.Utc);
            }
            if (entity.CreatedAt.Kind == DateTimeKind.Unspecified)
            {
                entity.CreatedAt = DateTime.SpecifyKind(entity.CreatedAt, DateTimeKind.Utc);
            }
        }
        
        await _transactionSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(Transaction entity)
    {
        //  Ensure DateTime is UTC before updating
        if (entity.Date.Kind == DateTimeKind.Unspecified)
        {
            entity.Date = DateTime.SpecifyKind(entity.Date, DateTimeKind.Utc);
        }
        if (entity.CreatedAt.Kind == DateTimeKind.Unspecified)
        {
            entity.CreatedAt = DateTime.SpecifyKind(entity.CreatedAt, DateTimeKind.Utc);
        }
        
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