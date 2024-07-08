namespace VisiProject.Infrastructure.Entities;

public class ConversationEntity
{
    public string ConversationId { get; set; }

    public string AdminId { get; set; }

    public string GroupName { get; set; }

    public long CreationTimeUnix { get; set; }

    public string SenderId { get; set; }

    public bool IsOnline { get; set; }

    public string? LastMessageId { get; set; }

    public UserEntity Admin { get; set; }

    public UserEntity Sender { get; set; }

    public MessageEntity? LastMessage { get; set; }

    public ICollection<UserConversationEntity> UserConversations { get; set; }

    public ICollection<MessageEntity> Messages { get; set; }
}