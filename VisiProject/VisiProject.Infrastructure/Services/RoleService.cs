using Microsoft.Extensions.Logging;
using Validation;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Services;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Contracts.Validators;
using VisiProject.Infrastructure.Filters;
using VisiProject.Infrastructure.Models;

namespace VisiProject.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly ILogger<RoleService> _logger;

    private readonly IRoleStore _roleStore;
    private readonly IRoleValidator _roleValidator;
    private readonly IAtomicScopeFactory _atomicScopeFactory;

    public RoleService(IRoleStore roleStore, IRoleValidator roleValidator, IAtomicScopeFactory atomicScopeFactory, ILogger<RoleService> logger)
    {
        _roleStore = roleStore;
        _roleValidator = roleValidator;
        _atomicScopeFactory = atomicScopeFactory;
        _logger = logger;
    }

    public async Task DeleteRoleAsync(string roleName)
    {
        Requires.NotNullOrEmpty(roleName, nameof(roleName));

        await using IAtomicScope atomicScope = _atomicScopeFactory.Create();

        RoleFilter filter = new()
        {
            Name = roleName
        };

        await _roleStore.DeleteAsync(filter, atomicScope);

        _logger.LogInformation("Deleted role successfully");

        await atomicScope.CommitAsync();
    }

    public async Task<IReadOnlyList<IRole>> GetRolesAsync()
    {
        await using IAtomicScope atomicScope = _atomicScopeFactory.CreateWithoutTransaction();

        RoleFilter filter = new();

        return await _roleStore.GetManyAsync(filter, atomicScope);
    }

    public async Task UpdateRoleAsync(string roleId, string roleName, string roleDescription)
    {
        Requires.NotNullOrEmpty(roleId, nameof(roleId));
        Requires.NotNullOrEmpty(roleName, nameof(roleName));
        Requires.NotNullOrEmpty(roleDescription, nameof(roleDescription));

        await using IAtomicScope atomicScope = _atomicScopeFactory.Create();

        IRole role = new Role()
        {
            RoleId = roleId,
            Name = roleName,
            Description = roleDescription
        };

        await _roleValidator.ValidateRoleUpdatesAsync(role, atomicScope);

        await _roleStore.UpdateAsync(role, atomicScope);

        _logger.LogInformation("Updated role successfully");

        await atomicScope.CommitAsync();
    }

    public async Task<IRole> CreateRoleAsync(string roleName, string roleDescription)
    {
        Requires.NotNullOrEmpty(roleName, nameof(roleName));
        Requires.NotNullOrEmpty(roleDescription, nameof(roleDescription));

        await using IAtomicScope atomicScope = _atomicScopeFactory.Create();

        await _roleValidator.ValidateRoleCreatesAsync(roleName, atomicScope);

        IRole role = new Role()
        {
            RoleId = Guid.NewGuid().ToString(),
            Name = roleName,
            Description = roleDescription
        };

        IRole roleResponse = await _roleStore.CreateAsync(role, atomicScope);

        _logger.LogInformation("Created role successfully");

        await atomicScope.CommitAsync();

        return roleResponse;
    }
}