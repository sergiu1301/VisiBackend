using VisiProject.Contracts.Models;
using VisiProject.Infrastructure.Entities;
using VisiProject.Infrastructure.Models;

namespace VisiProject.Infrastructure.Extensions;

public static class ConversationExtensions
{
    public static IConversation ToModel(this ConversationEntity model)
    {
        return new Conversation()
        {
            ConversationId = model.ConversationId,
            AdminId = model.AdminId,
            GroupName = model.GroupName,
            CreationTimeUnix = model.CreationTimeUnix,
            SenderId = model.SenderId,
            IsOnline = model.IsOnline,
            LastMessageId = model.LastMessageId,
            LastMessage = model.LastMessage?.ToModel(),
            Admin = model.Admin.ToModel(),
            Sender = model.Sender.ToModel(),
            UserConversations = model.UserConversations.Select(uc => uc.User.ToModel()).ToList(),
        };
    }

    public static ConversationEntity ToEntity(this IConversation entity)
    {
        return new ConversationEntity()
        {
            ConversationId = entity.ConversationId,
            AdminId = entity.AdminId,
            GroupName = entity.GroupName,
            CreationTimeUnix = entity.CreationTimeUnix,
            SenderId = entity.SenderId,
            IsOnline = entity.IsOnline,
            LastMessageId = entity.LastMessageId
        };
    }
}