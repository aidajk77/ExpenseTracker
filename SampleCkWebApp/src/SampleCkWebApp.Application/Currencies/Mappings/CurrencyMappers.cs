using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Currency;
using Domain.Entities;

namespace api.Mappers
{
    public static class CurrencyMappers
    {
        public static CurrencyDto ToDto(this Currency currency)
        {
            return new CurrencyDto
            {
                Id = currency.Id,
                Code = currency.Code,
                Name = currency.Name,
                Symbol = currency.Symbol,
                ExchangeRate = currency.ExchangeRate,
                CreatedAt = currency.CreatedAt
            };
        }

        public static Currency ToModel(this CreateCurrencyDto dto)
        {
            return new Currency
            {
                Code = dto.Code,
                Name = dto.Name,
                Symbol = dto.Symbol,
                ExchangeRate = dto.ExchangeRate
            };
        }
    }
}