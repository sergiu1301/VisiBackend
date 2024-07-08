using System.ComponentModel.DataAnnotations;

namespace VisiProject.API.Requests;

public class MessageRequest
{
    [Required]
    [StringLength(1000)]
    public string Content { get; set; }

    public string MessageType { get; set; }

    [Required]
    [StringLength(36)]
    public string ConversationId { get; set; }

    public long CreationTimeUnix { get; set; }
}