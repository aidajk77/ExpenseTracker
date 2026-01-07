using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Category;
using Domain.Entities;

namespace api.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedAt = category.CreatedAt
            };
        }

        public static Category ToModel(this CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }
    }
}