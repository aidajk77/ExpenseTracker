using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.PaymentMethod
{
    public class PaymentMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., "Cash", "Credit Card", "Bank Transfer"
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}