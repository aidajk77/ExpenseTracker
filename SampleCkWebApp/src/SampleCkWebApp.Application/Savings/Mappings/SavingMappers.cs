using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.Saving;
using Contracts.DTOs.UserSaving;
using Domain.Enums;
using Domain.Entities;

namespace api.Mappers
{
    public static class SavingMappers
    {
        public static SavingDto ToDto(this Saving saving)
        {
            return new SavingDto
            {
                Id = saving.Id,
                Name = saving.Name,
                Description = saving.Description,
                TargetAmount = saving.TargetAmount,
                CurrentAmount = saving.CurrentAmount,
                CreatedAt = saving.CreatedAt,
                TargetDate = saving.TargetDate,
                Status = saving.Status,
                Contributors = saving.UserSavings.Select(us => new UserSavingDto
                {
                    UserId = us.UserId,
                    ContributedAmount = us.ContributedAmount,
                    JoinedAt = us.JoinedAt
                }).ToList()
            };
        }

        public static Saving ToModel(this CreateSavingDto dto)
        {
            return new Saving
            {
                Name = dto.Name,
                Description = dto.Description,
                TargetAmount = dto.TargetAmount,
                CurrentAmount = 0,
                TargetDate = dto.TargetDate,
                Status = SavingStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UserSavings = new List<UserSaving>()
            };
        }
    }
}