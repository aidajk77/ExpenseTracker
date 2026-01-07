using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.UserSaving
{
    public class UpdateUserSavingDto
    {
        public decimal? ContributedAmount { get; set; }
    }
}