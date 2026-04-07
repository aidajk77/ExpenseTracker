using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;

namespace SampleCkWebApp.Application.PaymentMethod.Interfaces.Infrastructure;

public interface IPaymentMethodRepository : IRepository<Domain.Entities.PaymentMethod>
{
    Task<bool> PaymentMethodNameExistsAsync(string name);

    Task<Domain.Entities.PaymentMethod> GetPaymentMethodByNameAsync(string name);
}