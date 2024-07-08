using Microsoft.EntityFrameworkCore;
using Validation;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Infrastructure.Entities;
using VisiProject.Infrastructure.Extensions;
using VisiProject.Infrastructure.Models;
using IMessage = VisiProject.Contracts.Models.IMessage;

namespace VisiProject.Infrastructure.Stores;

public class MessageStore : IMessageStore
{
    public async Task<IMessage> UpsertAsync(IMessage message, IAtomicScope atomicScope)
    {
        Requires.NotNull(message, nameof(message));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        await context.Messages.AddAsync(message.ToEntity());

        ConversationEntity? conversation = await context.Conversations.Include(c => c.UserConversations).Where(c => c.ConversationId == message.ConversationId).FirstOrDefaultAsync();

        if (conversation == null)
        {
            throw new Exception();
        }

        conversation.LastMessageId = message.MessageId;

        context.Conversations.Update(conversation);

        var user = await context.Users.Where(u => u.UserId == message.SenderId).FirstOrDefaultAsync();

        await context.SaveChangesAsync();

        IMessage newMessage = new Message()
        {
            Content = message.Content,
            ConversationId = message.ConversationId,
            CreationTimeUnix = message.CreationTimeUnix,
            MessageId = message.MessageId,
            MessageType = message.MessageType,
            Sender = user!.ToModel(),
            SenderId = message.SenderId,
        };

        return newMessage;
    }

    public async Task<IReadOnlyList<IMessage>> GetManyAsync(string conversationId, int pageNumber, int pageSize, IAtomicScope atomicScope)
    {
        Requires.NotNull(conversationId, nameof(conversationId));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        IOrderedQueryable<MessageEntity> messagesQuery = context.Messages
            .Include(m => m.Sender)
            .Where(m => m.ConversationId == conversationId)
            .OrderByDescending(m => m.CreationTimeUnix);

        var a = messagesQuery.ToList();
        var messages = a
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).OrderBy(m => m.CreationTimeUnix);

        return messages.Select(m => m.ToModel()).ToList();
    }

    public async Task DeleteAsync(string messageId, IAtomicScope atomicScope)
    {
        Requires.NotNull(messageId, nameof(messageId));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        MessageEntity? message = await context.Messages.Where(m => m.MessageId == messageId).FirstOrDefaultAsync();

        context.Messages.Remove(message);

        IReadOnlyList<ConversationEntity> conversations = await context.Conversations.Where(c => c.LastMessageId == messageId).ToListAsync();

        foreach (var conversation in conversations)
        {
            conversation.LastMessageId = null;
        }

        context.Conversations.UpdateRange(conversations);

        await context.SaveChangesAsync();
    }
}