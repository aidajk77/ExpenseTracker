using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Currency
{
    public class CreateCurrencyDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal ExchangeRate { get; set; }
        public string Symbol { get; set; } = string.Empty;
    }
}