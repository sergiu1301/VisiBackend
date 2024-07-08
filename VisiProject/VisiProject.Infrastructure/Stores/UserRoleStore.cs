﻿using Microsoft.EntityFrameworkCore;
using Validation;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Infrastructure.Entities;
using VisiProject.Infrastructure.Exceptions;
using VisiProject.Infrastructure.Extensions;

namespace VisiProject.Infrastructure.Stores;

public class UserRoleStore : IUserRoleStore
{
    public async Task DeleteAsync(string userId, string roleName, IAtomicScope atomicScope)
    {
        Requires.NotNullOrEmpty(userId, nameof(userId));
        Requires.NotNullOrEmpty(roleName, nameof(roleName));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        UserEntity? user = context.Users.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                        .FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        UserRoleEntity userRole = user.UserRoles.FirstOrDefault(ur => roleName.Contains(ur.Role.Name))!;

        if (userRole == null)
        {
            throw new RoleNotFoundException();
        }

        context.UserRoles.Remove(userRole);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(string userId, string roleName, IAtomicScope atomicScope)
    {
        Requires.NotNullOrEmpty(userId, nameof(userId));
        Requires.NotNullOrEmpty(roleName, nameof(roleName));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        UserEntity? user = await context.Users.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                        .SingleOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var userRoles = user.UserRoles;

        context.UserRoles.RemoveRange(userRoles);

        RoleEntity? role = await context.Roles.SingleOrDefaultAsync(r => r.Name == roleName);

        if (role == null)
        {
            throw new RoleNotFoundException();
        }

        UserRoleEntity newUserRole = new()
        {
            UserId = userId,
            RoleId = role.RoleId
        };

        await context.UserRoles.AddAsync(newUserRole);

        await context.SaveChangesAsync();
    }

    public async Task<IRole> GetAsync(string userId, IAtomicScope atomicScope)
    {
        Requires.NotNull(userId, nameof(userId));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        UserEntity? user = await context.Users.Include(u => u.UserRoles)
                                              .ThenInclude(u => u.Role)
                                              .Where(u => u.UserId == userId)
                                              .SingleOrDefaultAsync();

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return user.UserRoles.Select(r => r.Role.ToModel()).SingleOrDefault()!;
    }
}