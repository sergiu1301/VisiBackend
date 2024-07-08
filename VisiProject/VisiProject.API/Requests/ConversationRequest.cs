using System.ComponentModel.DataAnnotations;

namespace VisiProject.API.Requests;

public class ConversationRequest
{
    [StringLength(36)]
    public string GroupName { get; set; }

    public long CreationTimeUnix { get; set; }

    public bool IsOnline { get; set; }

    public string? LastMessageId { get; set; }

    [Required]
    public ICollection<string> UserConversationIds { get; set; }
}