using Microsoft.EntityFrameworkCore;
using Validation;
using VisiProject.Contracts.Filters;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Infrastructure.Entities;
using VisiProject.Infrastructure.Exceptions;
using VisiProject.Infrastructure.Extensions;

namespace VisiProject.Infrastructure.Stores;

public class RoleStore : IRoleStore
{
    public async Task DeleteAsync(IRoleFilter filter, IAtomicScope atomicScope)
    {
        Requires.NotNull(filter, nameof(filter));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        RoleEntity? role = context.Roles.ApplyFiltering(filter).FirstOrDefault();

        if (role == null)
        {
            throw new RoleNotFoundException();
        }

        context.Roles.Remove(role);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<IRole>> GetManyAsync(IRoleFilter filter, IAtomicScope atomicScope)
    {
        Requires.NotNull(filter, nameof(filter));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        IReadOnlyList<RoleEntity> roles = await context.Roles.ApplyFiltering(filter).ToListAsync();

        return roles.Select(r => r.ToModel()).ToList();
    }

    public async Task UpdateAsync(IRole role, IAtomicScope atomicScope)
    {
        Requires.NotNull(role, nameof(role));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        context.Roles.Update(role.ToEntity());
        await context.SaveChangesAsync();
    }

    public async Task<IRole> CreateAsync(IRole role, IAtomicScope atomicScope)
    {
        Requires.NotNull(role, nameof(role));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        context.Roles.Add(role.ToEntity());
        await context.SaveChangesAsync();

        return role;
    }

    public async Task<bool> ExistsAsync(IRoleFilter filter, IAtomicScope atomicScope)
    {
        Requires.NotNull(filter, nameof(filter));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        return await context.Roles.ApplyFiltering(filter)
                                  .AnyAsync();
    }
}