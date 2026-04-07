using ErrorOr;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.PaymentMethods.Interfaces.Application;
using Contracts.DTOs.PaymentMethod;
using Domain.Entities;
using api.Mappers;
using SampleCkWebApp.Application.PaymentMethods;
using Domain.Errors;
using SampleCkWebApp.Application.PaymentMethod.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.PaymentMethod;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly PaymentMethodValidator _paymentMethodValidator;

    public PaymentMethodService(
        IPaymentMethodRepository paymentMethodRepository,
        PaymentMethodValidator paymentMethodValidator)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _paymentMethodValidator = paymentMethodValidator;
    }
    public async Task<ErrorOr<IEnumerable<PaymentMethodDto>>> GetAllPaymentMethodsAsync(CancellationToken cancellationToken = default)
    {
        var methods = await _paymentMethodRepository.GetAllAsync();
        return methods.Select(m => m.ToDto()).ToList();
    }

    public async Task<ErrorOr<PaymentMethodDto>> GetPaymentMethodByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var method = await _paymentMethodRepository.GetByIdAsync(id);
        if (method == null)
            return PaymentMethodErrors.NotFound;

        return method.ToDto();
    }

    public async Task<ErrorOr<PaymentMethodDto>> CreatePaymentMethodAsync(CreatePaymentMethodDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _paymentMethodValidator.ValidateCreatePaymentMethod(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var nameExists = await _paymentMethodRepository.PaymentMethodNameExistsAsync(request.Name);
        if (nameExists)
            return PaymentMethodErrors.DuplicateName;

        var paymentMethod = request.ToModel();
        
        await _paymentMethodRepository.AddAsync(paymentMethod);
        await _paymentMethodRepository.SaveChangesAsync();

        return paymentMethod.ToDto();
    }

    public async Task<ErrorOr<PaymentMethodDto>> UpdatePaymentMethodAsync(int id, UpdatePaymentMethodDto request, CancellationToken cancellationToken = default)
    {
        //  Validate input using validator
        var validationResult = _paymentMethodValidator.ValidateUpdatePaymentMethod(request);
        if (validationResult.IsError)
            return validationResult.Errors;

        var paymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
        if (paymentMethod == null)
            return PaymentMethodErrors.NotFound;
        
        //  Check for duplicate name if name is being updated
        if (!string.IsNullOrEmpty(request.Name) && request.Name.Trim() != paymentMethod.Name)
        {
            var nameExists = await _paymentMethodRepository.PaymentMethodNameExistsAsync(request.Name);
            if (nameExists)
                return PaymentMethodErrors.DuplicateName;
        }

        if (!string.IsNullOrEmpty(request.Name))
            paymentMethod.Name = request.Name;

        if (request.Description != null)
            paymentMethod.Description = request.Description;

        await _paymentMethodRepository.UpdateAsync(paymentMethod);
        await _paymentMethodRepository.SaveChangesAsync();

        return paymentMethod.ToDto();
    }

    public async Task<ErrorOr<Success>> DeletePaymentMethodAsync(int id, CancellationToken cancellationToken = default)
    {
        var method = await _paymentMethodRepository.GetByIdAsync(id);
        if (method == null)
            return PaymentMethodErrors.NotFound;

        await _paymentMethodRepository.DeleteAsync(method);
        await _paymentMethodRepository.SaveChangesAsync();

        return Result.Success;
        
    }
}
