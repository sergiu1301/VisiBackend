using VisiProject.Contracts.Models;
using VisiProject.Contracts.Transactions;

namespace VisiProject.Contracts.Stores;

public interface IConversationStore
{
    Task<IConversation> CreateAsync(IConversation conversation, IAtomicScope atomicScope);
    Task<IReadOnlyList<IConversation>> GetManyAsync(string userId, IAtomicScope atomicScope);
    Task UpdateAsync(string userId, string conversationId, IAtomicScope atomicScope);
}