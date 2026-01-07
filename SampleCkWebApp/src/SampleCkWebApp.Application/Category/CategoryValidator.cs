using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Category;
using Domain.Errors;
using ErrorOr;

namespace SampleCkWebApp.Application.Category
{
    public class CategoryValidator
    {
        public ErrorOr<Success> ValidateCreateCategory(CreateCategoryDto request)
        {
            var errors = new List<Error>();

            //  Validate Name
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add(CategoryErrors.NameRequired);
            else if (request.Name.Length > 100 || request.Name.Length < 1)
                errors.Add(CategoryErrors.InvalidName);

            return errors.Count > 0 ? errors : Result.Success;
        }

        public ErrorOr<Success> ValidateUpdateCategory(UpdateCategoryDto request)
        {
            var errors = new List<Error>();

            //  Validate Name if provided
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                if (request.Name.Length > 100 || request.Name.Length < 1)
                    errors.Add(CategoryErrors.InvalidName);
            }

            return errors.Count > 0 ? errors : Result.Success;
        }
            
    }
}