using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.User
{
    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? CurrencyId { get; set; }
    }
}