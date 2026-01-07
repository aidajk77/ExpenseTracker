using ErrorOr;
using Contracts.DTOs.PaymentMethod;

namespace SampleCkWebApp.Application.PaymentMethods.Interfaces.Application;

public interface IPaymentMethodService
{
    Task<ErrorOr<IEnumerable<PaymentMethodDto>>> GetAllPaymentMethodsAsync(CancellationToken cancellationToken = default);
    Task<ErrorOr<PaymentMethodDto>> GetPaymentMethodByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ErrorOr<PaymentMethodDto>> CreatePaymentMethodAsync(CreatePaymentMethodDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<PaymentMethodDto>> UpdatePaymentMethodAsync(int id, UpdatePaymentMethodDto request, CancellationToken cancellationToken = default);
    Task<ErrorOr<Success>> DeletePaymentMethodAsync(int id, CancellationToken cancellationToken = default);
}
