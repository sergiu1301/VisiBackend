using Validation;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Services;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Infrastructure.Models;

namespace VisiProject.Infrastructure.Services;

public class ConversationService : IConversationService
{
    private readonly IConversationStore _conversationStore;
    private readonly IAtomicScopeFactory _atomicScopeFactory;

    public ConversationService(IConversationStore conversationStore , IAtomicScopeFactory atomicScopeFactory)
    {
        _conversationStore = conversationStore;
        _atomicScopeFactory = atomicScopeFactory;
    }

    public async Task<IConversation> CreateConversationAsync(string adminId, string groupName, long creationTimeUnix,
        string senderId, bool isOnline, string? lastMessageId, ICollection<string> userConversationIds)
    {
        Requires.NotNullOrEmpty(adminId, nameof(adminId));
        Requires.NotNullOrEmpty(groupName, nameof(groupName));
        Requires.NotNullOrEmpty(senderId, nameof(senderId));
        Requires.NotNullOrEmpty(userConversationIds, nameof(userConversationIds));

        IConversation conversation = new Conversation()
        {
            ConversationId = Guid.NewGuid().ToString(),
            AdminId = adminId,
            GroupName = groupName,
            CreationTimeUnix = creationTimeUnix,
            SenderId = senderId,
            LastMessageId = lastMessageId,
            IsOnline = isOnline,
            UserConversationIds = userConversationIds
        };

        await using IAtomicScope atomicScope = _atomicScopeFactory.Create();

        IConversation conversationResponse = await _conversationStore.CreateAsync(conversation, atomicScope);

        await atomicScope.CommitAsync();

        return conversationResponse;
    }

    public async Task<IReadOnlyList<IConversation>> GetConversationsAsync(string userId)
    {
        Requires.NotNullOrEmpty(userId, nameof(userId));

        await using IAtomicScope atomicScope = _atomicScopeFactory.CreateWithoutTransaction();

        return await _conversationStore.GetManyAsync(userId, atomicScope);
    }

    public async Task UpdateConversationAsync(string userId, string conversationId)
    {
        Requires.NotNullOrEmpty(userId, nameof(userId));
        Requires.NotNullOrEmpty(conversationId, nameof(conversationId));

        await using IAtomicScope atomicScope = _atomicScopeFactory.Create();

        await _conversationStore.UpdateAsync(userId, conversationId, atomicScope);

        await atomicScope.CommitAsync();
    }
}