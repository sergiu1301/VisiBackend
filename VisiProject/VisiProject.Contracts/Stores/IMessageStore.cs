using VisiProject.Contracts.Models;
using VisiProject.Contracts.Transactions;

namespace VisiProject.Contracts.Stores;

public interface IMessageStore
{
    Task<IMessage> UpsertAsync(IMessage message, IAtomicScope atomicScope);
    Task<IReadOnlyList<IMessage>> GetManyAsync(string conversationId, int pageNumber, int pageSize, IAtomicScope atomicScope);
    Task DeleteAsync(string messageId, IAtomicScope atomicScope);
}