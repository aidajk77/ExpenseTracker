using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Currency
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty; 
        public string Symbol { get; set; } = string.Empty; 
        public decimal ExchangeRate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}