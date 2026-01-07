using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.UserSaving
{
    public class CreateUserSavingDto
    {
        public int UserId { get; set; }
        public int SavingId { get; set; }
    }
}