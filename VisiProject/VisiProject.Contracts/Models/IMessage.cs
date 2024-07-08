namespace VisiProject.Contracts.Models;

public interface IMessage
{
    string MessageId { get; }

    string Content { get; }

    string SenderId { get; }

    IUser Sender { get; }

    string MessageType { get; }

    string ConversationId { get; }

    long CreationTimeUnix { get; }
}