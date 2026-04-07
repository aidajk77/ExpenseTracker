using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.Transaction.Interfaces.Infrastructure
{
    public interface ITransactionRepository : IRepository<Domain.Entities.Transaction>
    {
        Task<(IEnumerable<Domain.Entities.Transaction> transactions, int totalCount)> GetUserTransactionsPagedAsync(
            int userId,
            int page,
            int limit,
            TransactionType? type = null,
            int? categoryId = null,
            int? savingId = null,
            DateTime? startDate = null,
            DateTime? endDate = null);
        
        Task<decimal> GetUserMonthlyTotalAsync(
            int userId,
            TransactionType type,
            int month,
            int year);

        Task<decimal> GetUserTotalByTypeAndDateRangeAsync(
            int userId,
            TransactionType type,
            DateTime startDate,
            DateTime endDate);
        
        Task<(bool userExists, bool paymentMethodExists, bool categoryExists, bool savingExists, bool userInSaving)> 
        ValidateTransactionDependenciesAsync(
            int userId,
            int paymentMethodId,
            int? categoryId,
            int? savingId);
        
        Task<IEnumerable<Domain.Entities.Transaction>> GetBySavingIdAsync(int savingId);
        Task<IEnumerable<Domain.Entities.Transaction>> GetByCategoryIdAsync(int categoryId);
    }
        
}