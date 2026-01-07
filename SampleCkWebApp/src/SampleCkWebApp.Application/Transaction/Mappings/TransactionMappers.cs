using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Transaction;
using Domain.Entities;

namespace api.Mappers
{
    public static class TransactionMappers
    {
        public static TransactionDto ToDto(this Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                CategoryId = transaction.CategoryId,
                SavingId = transaction.SavingId,
                PaymentMethodId = transaction.PaymentMethodId,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Description = transaction.Description,
                Date = transaction.Date,
                CreatedAt = transaction.CreatedAt
            };
        }

        public static Transaction ToModel(this CreateTransactionDto dto)
        {
            return new Transaction
            {
                UserId=dto.UserId,
                CategoryId = dto.CategoryId,
                PaymentMethodId = dto.PaymentMethodId,
                SavingId = dto.SavingId,
                Amount = dto.Amount,
                Type = dto.Type,
                Description = dto.Description,
                Date = dto.Date
            };
        }
    }
}