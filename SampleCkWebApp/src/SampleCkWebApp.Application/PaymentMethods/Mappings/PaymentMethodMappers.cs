using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.PaymentMethod;
using Domain.Entities;

namespace api.Mappers
{
    public static class PaymentMethodMappers
    {
        public static PaymentMethodDto ToDto(this PaymentMethod paymentMethod)
        {
            return new PaymentMethodDto
            {
                Id = paymentMethod.Id,
                Name = paymentMethod.Name,
                Description = paymentMethod.Description,
                CreatedAt = paymentMethod.CreatedAt
            };
        }

        public static PaymentMethod ToModel(this CreatePaymentMethodDto dto)
        {
            return new PaymentMethod
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }
    }
}