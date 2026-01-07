using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.DTOs.Category
{
    public class CreateCategoryDto
    {
        public required string Name { get; set; }
    }
}