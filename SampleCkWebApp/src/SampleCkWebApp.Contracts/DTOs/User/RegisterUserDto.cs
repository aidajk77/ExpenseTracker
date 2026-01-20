using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums;

namespace Contracts.DTOs.User
{
    public class RegisterUserDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role Role { get; set; } = Role.User;

        public int CurrencyId { get; set; }  
    }
}