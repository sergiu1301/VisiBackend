namespace VisiProject.Infrastructure.Models;

using VisiProject.Contracts.Models;
using IMessage = Contracts.Models.IMessage;

public class Message: IMessage
{
    public string MessageId { get; set; }

    public string Content { get; set; }

    public string SenderId { get; set; }

    public IUser Sender { get; set; }

    public string MessageType { get; set; }

    public string ConversationId { get; set; }

    public long CreationTimeUnix { get; set; }
}