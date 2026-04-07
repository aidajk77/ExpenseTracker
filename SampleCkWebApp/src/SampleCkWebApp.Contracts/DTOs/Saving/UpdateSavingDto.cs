using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Contracts.DTOs.Saving
{
    public class UpdateSavingDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? TargetAmount { get; set; }
        public decimal? CurrentAmount { get; set; }
        public DateTime? TargetDate { get; set; }
        public SavingStatus? Status { get; set; }
        public List<int>? UserIds { get; set; }
    }
}