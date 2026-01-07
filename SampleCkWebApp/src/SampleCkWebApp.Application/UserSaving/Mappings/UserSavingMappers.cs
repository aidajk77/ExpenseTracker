using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DTOs.UserSaving;
using Domain.Entities;

namespace api.Mappers
{
    public static class UserSavingMappers
    {
        public static UserSavingDto ToDto(this UserSaving userSaving, decimal savingCurrentAmount = 0)
        {
            return new UserSavingDto
            {
                UserId = userSaving.UserId,
                SavingId = userSaving.SavingId,
                ContributedAmount = userSaving.ContributedAmount,
                JoinedAt = userSaving.JoinedAt
            };
        }

        public static UserSaving ToModel(this CreateUserSavingDto dto)
        {
            return new UserSaving
            {
                UserId = dto.UserId,
                SavingId = dto.SavingId,
                ContributedAmount = 0,
                JoinedAt = DateTime.UtcNow
            };
        }
    }
}