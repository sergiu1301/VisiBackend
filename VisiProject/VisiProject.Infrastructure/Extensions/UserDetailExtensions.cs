using VisiProject.Contracts.Models;
using VisiProject.Infrastructure.Entities;

namespace VisiProject.Infrastructure.Extensions;

public static class UserDetailExtensions
{
    public static UserEntity ToEntity(this IUserDetail model)
    {
        return new UserEntity()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            EmailConfirmed = model.EmailConfirmed,
            IsBlocked = model.IsBlocked,
            UserId = model.UserId,
            UserName = model.UserName,
            PasswordHash = model.PasswordHash,
            Salt = model.Salt,
        };
    }
}