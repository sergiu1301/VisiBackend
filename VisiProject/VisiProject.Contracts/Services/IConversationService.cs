using VisiProject.Contracts.Models;

namespace VisiProject.Contracts.Services;

public interface IConversationService
{
    Task<IConversation> CreateConversationAsync(
        string adminId, 
        string groupName, 
        long creationTimeUnix, 
        string senderId, 
        bool isOnline,
        string? lastMessageId,
        ICollection<string> userConversationIds);

    Task<IReadOnlyList<IConversation>> GetConversationsAsync(string userId);
    Task UpdateConversationAsync(string userId, string conversationId);
}