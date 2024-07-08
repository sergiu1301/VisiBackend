using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using VisiProject.API.Requests;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Services;
using Microsoft.AspNetCore.SignalR;

namespace VisiProject.API.Controllers;

/// <summary>
/// Message Controller
/// </summary>
[Route("/v{version:apiVersion}/message")]
[ApiVersion("1.0")]
public class MessageController: ApiController
{
    private readonly IMessageService _messageService;
    private readonly IContextService _contextService;
    private readonly IHubContext<ChatHub> _chatHubContext;

    /// <summary>
    /// Message Constructor
    /// </summary>
    /// <param name="messageService">Message Service</param>
    /// <param name="contextService">Context Service</param>
    public MessageController(IMessageService messageService, IContextService contextService, IHubContext<ChatHub> chatHubContext)
    {
        _messageService = messageService;
        _contextService = contextService;
        _chatHubContext = chatHubContext;
    }

    /// <summary>
    /// Create a message
    /// </summary>
    /// <response code="200">Returns the message.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Not Found.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpsertMessageAsync([FromBody][Required] MessageRequest request)
    {
        string userId = await _contextService.GetCurrentUserIdAsync();
        IMessage message = await _messageService.UpsertMessageAsync(request.ConversationId, request.Content, request.CreationTimeUnix, request.MessageType, userId);

        await _chatHubContext.Clients.Group(request.ConversationId).SendAsync("ReceiveMessage", message);

        return Ok(message);
    }

    /// <summary>
    /// Delete a message
    /// </summary>
    /// <response code="204">No content.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Not Found.</response>
    [HttpDelete("{messageId}/conversation/{conversationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMessageAsync([FromRoute][Required] string messageId, [FromRoute][Required] string conversationId)
    {
        await _messageService.DeleteMessageAsync(messageId);

        await _chatHubContext.Clients.Group(conversationId).SendAsync("MessageDeleted", messageId);

        return NoContent();
    }

    /// <summary>
    /// Get messages of a conversation
    /// </summary>
    /// <response code="200">Return the messages.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Not Found.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMessagesAsync([FromQuery] string conversationId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        IReadOnlyList<IMessage> messages = await _messageService.GetMessagesAsync(conversationId, pageNumber, pageSize);

        return Ok(messages);
    }
}