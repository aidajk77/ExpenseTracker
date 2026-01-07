using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.UserSaving;
using Domain.Enums;

namespace Contracts.DTOs.Saving
{
    public class SavingDto
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string? Description { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal RemainingAmount => TargetAmount - CurrentAmount;                                                                     // ← NEW
        public DateTime CreatedAt { get; set; }
        public DateTime? TargetDate { get; set; }
        public SavingStatus Status { get; set; }
        public List<UserSavingDto> Contributors { get; set; } = new(); 
    }

    
}