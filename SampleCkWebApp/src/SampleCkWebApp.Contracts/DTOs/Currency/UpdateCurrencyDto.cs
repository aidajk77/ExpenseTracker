using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Currency
{
    public class UpdateCurrencyDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
    }
}