using Microsoft.EntityFrameworkCore;
using Validation;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Infrastructure.Entities;
using VisiProject.Infrastructure.Extensions;
using VisiProject.Infrastructure.Models;

namespace VisiProject.Infrastructure.Stores;

public class ConversationStore : IConversationStore
{
    public async Task<IConversation> CreateAsync(IConversation conversation, IAtomicScope atomicScope)
    {
        Requires.NotNull(conversation, nameof(conversation));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        Conversation conversationDb = new()
        {
            Admin = conversation.Admin,
            AdminId = conversation.AdminId,
            CreationTimeUnix = conversation.CreationTimeUnix,
            ConversationId = conversation.ConversationId,
            IsOnline = conversation.IsOnline,
            LastMessage = conversation.LastMessage,
            LastMessageId = conversation.LastMessageId,
            Sender = conversation.Sender,
            SenderId = conversation.SenderId,
            GroupName = conversation.GroupName,
        };

        if (conversation.UserConversationIds.Count == 2)
        {
            IReadOnlyList<UserEntity> users = await context.Users.Where(c => conversation.UserConversationIds.Contains(c.UserId))
                                                                 .ToListAsync();

            string groupName = string.Empty;
            foreach (var user in users)
            {
                groupName += user.UserName + "/";
            }

            conversationDb.GroupName = groupName;
        }
        context.Conversations.Add(conversationDb.ToEntity());

        string conversationId = conversation.ConversationId;

        foreach (string userConversation in conversation.UserConversationIds)
        {
            UserConversationEntity userConversationEntity = new()
            {
                ConversationId = conversationId,
                UserId = userConversation
            };

            await context.UserConversations.AddAsync(userConversationEntity);
        }
        
        await context.SaveChangesAsync();

        return conversation;
    }

    public async Task<IReadOnlyList<IConversation>> GetManyAsync(string userId, IAtomicScope atomicScope)
    {
        Requires.NotNull(userId, nameof(userId));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        IReadOnlyList<ConversationEntity> conversations = await context.Conversations.Include(c => c.LastMessage)
                                                                                     .Include(c => c.UserConversations).ThenInclude(uc => uc.User).ThenInclude(c => c.UserConversations)
                                                                                     .Where(c => c.UserConversations.Select(uc => uc.UserId).Contains(userId))
                                                                                     .ToListAsync();

        return conversations.Select(c => c.ToModel()).ToList();
    }

    public async Task UpdateAsync(string userId, string conversationId, IAtomicScope atomicScope)
    {
        Requires.NotNull(userId, nameof(userId));
        Requires.NotNull(conversationId, nameof(conversationId));
        Requires.NotNull(atomicScope, nameof(atomicScope));

        ApplicationDbContext context = await atomicScope.ToDbContextAsync<ApplicationDbContext>(options => new ApplicationDbContext(options));

        UserConversationEntity? userConversation = await context.UserConversations
            .Where(uc => uc.UserId == userId && uc.ConversationId == conversationId).FirstOrDefaultAsync();
        
        context.UserConversations.Remove(userConversation);

        await context.SaveChangesAsync();
    }
}