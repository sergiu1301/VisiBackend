namespace VisiProject.Infrastructure.Entities;

public class UserConversationEntity
{
    public string UserId { get; set; }

    public string ConversationId { get; set; }

    public UserEntity User { get; set; }

    public ConversationEntity Conversation { get; set; }
}