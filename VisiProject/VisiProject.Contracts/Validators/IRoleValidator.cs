using VisiProject.Contracts.Models;
using VisiProject.Contracts.Transactions;

namespace VisiProject.Contracts.Validators;

public interface IRoleValidator
{
    Task ValidateRoleUpdatesAsync(IRole role, IAtomicScope atomicScope);
    Task ValidateRoleCreatesAsync(string roleName, IAtomicScope atomicScope);
}