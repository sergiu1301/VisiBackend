using System.ComponentModel.DataAnnotations;

namespace VisiProject.Infrastructure.Entities;

public class MessageEntity
{
    public string MessageId { get; set; }

    public string Content { get; set; }

    public string SenderId { get; set; }

    public string MessageType { get; set; }

    public string ConversationId { get; set; }

    public long CreationTimeUnix { get; set; }

    public UserEntity Sender { get; set; }

    public ConversationEntity Conversation { get; set; }
}