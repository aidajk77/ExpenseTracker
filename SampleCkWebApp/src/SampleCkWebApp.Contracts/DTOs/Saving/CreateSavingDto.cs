using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Saving
{
    public class CreateSavingDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal TargetAmount { get; set; }
        public DateTime? TargetDate { get; set; }
        public List<int> UserIds { get; set; }
    }
}