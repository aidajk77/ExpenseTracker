using ErrorOr;
using Contracts.DTOs.Transaction;

namespace SampleCkWebApp.Application.Transaction.Interfaces.Application;

public interface ITransactionService
{
    Task<ErrorOr<IEnumerable<TransactionDto>>> GetAllTransactionsAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<TransactionDto>> GetTransactionByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<TransactionDto>> CreateTransactionAsync(CreateTransactionDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<TransactionDto>> UpdateTransactionAsync(int id, UpdateTransactionDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeleteTransactionAsync(int id, CancellationToken cancellationToken = default);
}
