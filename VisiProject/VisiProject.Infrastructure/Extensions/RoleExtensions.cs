using VisiProject.Contracts.Models;
using VisiProject.Infrastructure.Entities;
using VisiProject.Infrastructure.Models;

namespace VisiProject.Infrastructure.Extensions;

public static class RoleExtensions
{
    public static IRole ToModel(this RoleEntity model)
    {
        return new Role()
        {
            RoleId = model.RoleId,
            Name = model.Name,
            Description = model.Description
        };
    }

    public static RoleEntity ToEntity(this IRole entity)
    {
        return new RoleEntity()
        {
            RoleId = entity.RoleId,
            Name = entity.Name,
            Description = entity.Description
        };
    }
}