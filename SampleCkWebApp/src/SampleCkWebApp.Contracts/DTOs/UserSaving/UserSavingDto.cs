using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.UserSaving
{
    public class UserSavingDto
    {
        public int UserId { get; set; }
        public int SavingId { get; set; }
        public decimal ContributedAmount { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}