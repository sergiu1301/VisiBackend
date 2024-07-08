using VisiProject.Contracts.Models;
using VisiProject.Contracts.Transactions;

namespace VisiProject.Contracts.Stores;

public interface IUserRoleStore
{
    Task DeleteAsync(string userId, string roleName, IAtomicScope atomicScope);
    Task AddAsync(string userId, string roleName, IAtomicScope atomicScope);
    Task<IRole> GetAsync(string userId, IAtomicScope atomicScope);
}