using VisiProject.Contracts.Models;
using VisiProject.Infrastructure.Entities;
using VisiProject.Infrastructure.Models;
using IMessage = VisiProject.Contracts.Models.IMessage;

namespace VisiProject.Infrastructure.Extensions;

public static class MessageExtensions
{
    public static IMessage ToModel(this MessageEntity model)
    {
        return new Message()
        {
            MessageId = model.MessageId,
            Content = model.Content,
            MessageType = model.MessageType,
            ConversationId = model.ConversationId,
            CreationTimeUnix = model.CreationTimeUnix,
            SenderId = model.SenderId,
            Sender = model.Sender.ToModel()
        };
    }

    public static MessageEntity ToEntity(this IMessage entity)
    {
        return new MessageEntity()
        {
            MessageId = entity.MessageId,
            Content = entity.Content,
            MessageType = entity.MessageType,
            ConversationId = entity.ConversationId,
            CreationTimeUnix = entity.CreationTimeUnix,
            SenderId = entity.SenderId,
        };
    }
}