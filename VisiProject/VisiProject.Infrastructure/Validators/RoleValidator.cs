using VisiProject.Contracts.Models;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Contracts.Validators;
using VisiProject.Infrastructure.Exceptions;
using VisiProject.Infrastructure.Filters;

namespace VisiProject.Infrastructure.Validators;

public class RoleValidator : IRoleValidator
{
    private readonly IRoleStore _roleStore;

    public RoleValidator(IRoleStore roleStore)
    {
        _roleStore = roleStore;
    }

    public async Task ValidateRoleUpdatesAsync(IRole role, IAtomicScope atomicScope)
    {
        RoleFilter filter = new()
        {
            Name = role.Name
        };

        IReadOnlyList<IRole> searchedRoles = await _roleStore.GetManyAsync(filter, atomicScope);

        if (searchedRoles.Count > 0)
        {
            if (searchedRoles.First().RoleId != role.RoleId)
            {
                throw new RoleAlreadyExistsException();
            }
        }
    }

    public async Task ValidateRoleCreatesAsync(string roleName, IAtomicScope atomicScope)
    {
        RoleFilter filter = new()
        {
            Name = roleName
        };

        bool roleExists = await _roleStore.ExistsAsync(filter, atomicScope);

        if (roleExists)
        {
            throw new RoleAlreadyExistsException();
        }
    }
}