using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public string? Name { get; set; }
        public decimal? AllTimeAmount { get; set; }
    }
}