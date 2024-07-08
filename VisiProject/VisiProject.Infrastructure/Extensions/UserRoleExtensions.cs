using VisiProject.Contracts.Models;
using VisiProject.Infrastructure.Entities;

namespace VisiProject.Infrastructure.Extensions;

public static class UserRoleExtensions
{
    public static UserRoleEntity ToEntity(this IUserRole entity)
    {
        return new UserRoleEntity()
        {
            RoleId = entity.RoleId,
            UserId = entity.UserId
        };
    }
}