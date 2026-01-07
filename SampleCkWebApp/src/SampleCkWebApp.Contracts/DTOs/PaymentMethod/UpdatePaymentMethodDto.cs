using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.PaymentMethod
{
    public class UpdatePaymentMethodDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}