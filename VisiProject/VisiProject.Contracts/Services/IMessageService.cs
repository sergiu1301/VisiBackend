using VisiProject.Contracts.Models;

namespace VisiProject.Contracts.Services;

public interface IMessageService
{
    Task<IMessage> UpsertMessageAsync(
        string conversationId,
        string content,
        long creationTimeUnix,
        string messageType,
        string userId);

    Task<IReadOnlyList<IMessage>> GetMessagesAsync(string conversationId, int pageNumber, int pageSize);
    Task DeleteMessageAsync(string messageId);
}