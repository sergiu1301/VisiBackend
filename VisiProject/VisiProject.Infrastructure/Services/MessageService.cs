using Validation;
using VisiProject.Contracts.Services;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Infrastructure.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using VisiProject.Infrastructure.Stores;
using IMessage = VisiProject.Contracts.Models.IMessage;
using NServiceBus;

namespace VisiProject.Infrastructure.Services;

public class MessageService: IMessageService
{
    private readonly IMessageStore _messageStore;
    private readonly IAtomicScopeFactory _atomicScopeFactory;

    public MessageService(IMessageStore messageStore, IAtomicScopeFactory atomicScopeFactory)
    {
        _messageStore = messageStore;
        _atomicScopeFactory = atomicScopeFactory;
    }

    public async Task<IMessage> UpsertMessageAsync(string conversationId, string content, long creationTimeUnix, string messageType,
        string userId)
    {
        Requires.NotNullOrEmpty(conversationId, nameof(conversationId));
        Requires.NotNullOrEmpty(content, nameof(content));
        Requires.NotNullOrEmpty(messageType, nameof(messageType));
        Requires.NotNullOrEmpty(userId, nameof(userId));

        IMessage message = new Message()
        {
            ConversationId = conversationId, 
            Content = content, 
            CreationTimeUnix = creationTimeUnix, 
            SenderId = userId,
            MessageType = messageType,
            MessageId = Guid.NewGuid().ToString()
        };

        await using IAtomicScope atomicScope = _atomicScopeFactory.Create();

        IMessage messageResponse = await _messageStore.UpsertAsync(message, atomicScope);

        await atomicScope.CommitAsync();

        return messageResponse;
    }

    public async Task<IReadOnlyList<IMessage>> GetMessagesAsync(string conversationId, int pageNumber, int pageSize)
    {
        Requires.NotNullOrEmpty(conversationId, nameof(conversationId));

        await using IAtomicScope atomicScope = _atomicScopeFactory.CreateWithoutTransaction();

        return await _messageStore.GetManyAsync(conversationId, pageNumber, pageSize, atomicScope);
    }

    public async Task DeleteMessageAsync(string messageId)
    {
        Requires.NotNullOrEmpty(messageId, nameof(messageId));

        await using IAtomicScope atomicScope = _atomicScopeFactory.Create();

        await _messageStore.DeleteAsync(messageId, atomicScope);

        await atomicScope.CommitAsync();
    }
}