using VisiProject.Contracts.Filters;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Transactions;

namespace VisiProject.Contracts.Stores;

public interface IRoleStore
{
    Task DeleteAsync(IRoleFilter filter, IAtomicScope atomicScope);
    Task<IReadOnlyList<IRole>> GetManyAsync(IRoleFilter filter, IAtomicScope atomicScope);
    Task UpdateAsync(IRole role, IAtomicScope atomicScope);
    Task<IRole> CreateAsync(IRole role, IAtomicScope atomicScope);
    Task<bool> ExistsAsync(IRoleFilter filter, IAtomicScope atomicScope);
}