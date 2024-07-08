using VisiProject.Contracts.Models;
using IMessage = VisiProject.Contracts.Models.IMessage;

namespace VisiProject.Infrastructure.Models;

public class Conversation : IConversation
{
    public string ConversationId { get; set; }

    public string AdminId { get; set; }

    public IUser Admin { get; set; }

    public string GroupName { get; set; }

    public long CreationTimeUnix { get; set; }

    public string SenderId { get; set; }

    public IUser Sender { get; set; }

    public bool IsOnline { get; set; }

    public string? LastMessageId { get; set; }

    public IMessage? LastMessage { get; set; }

    public ICollection<string> UserConversationIds { get; set; }
    public ICollection<IUser> UserConversations { get; set; }
}