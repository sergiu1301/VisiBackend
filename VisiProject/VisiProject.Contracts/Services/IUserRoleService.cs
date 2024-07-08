using VisiProject.Contracts.Models;
using VisiProject.Contracts.Transactions;

namespace VisiProject.Contracts.Services;

public interface IUserRoleService
{
    Task DeleteUserRoleAsync(string userId, string roleName);
    Task AddUserRoleAsync(string userId, string roleName);
    Task AddUserRoleAsync(string userId, string roleName, IAtomicScope atomicScope);
    Task<IRole> GetUserRoleAsync(string userId);
}