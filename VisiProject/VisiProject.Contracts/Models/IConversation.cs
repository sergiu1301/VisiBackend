namespace VisiProject.Contracts.Models;

public interface IConversation
{
    string ConversationId { get; }

    string AdminId { get; }

    public IUser Admin { get; }

    string GroupName { get; }

    long CreationTimeUnix { get; }

    string SenderId { get; }

    public IUser Sender { get; }

    bool IsOnline { get; }

    string? LastMessageId { get; }

    public IMessage LastMessage { get; }

    ICollection<string> UserConversationIds { get; }

    ICollection<IUser> UserConversations { get; set; }
}