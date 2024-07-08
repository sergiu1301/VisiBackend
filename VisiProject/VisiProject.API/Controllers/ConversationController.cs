using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VisiProject.API.Requests;
using VisiProject.Contracts.Models;
using VisiProject.Contracts.Services;

namespace VisiProject.API.Controllers;

/// <summary>
/// Conversation Controller
/// </summary>
[Route("/v{version:apiVersion}/conversation")]
[Authorize(Policy = Policies.Policies.UserOrAdminPolicy)]
[ApiVersion("1.0")]
public class ConversationController : ApiController
{
    private readonly IConversationService _conversationService;
    private readonly IContextService _contextService;

    /// <summary>
    /// Conversation Constructor
    /// </summary>
    /// <param name="conversationService">Conversation Service</param>
    /// <param name="contextService">Context Service</param>
    public ConversationController(IConversationService conversationService, IContextService contextService)
    {
        _conversationService = conversationService;
        _contextService = contextService;
    }

    /// <summary>
    /// Create a conversation
    /// </summary>
    /// <response code="200">Returns the conversation.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Not Found.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateConversationAsync([FromBody][Required] ConversationRequest request)
    {
        string userId = await _contextService.GetCurrentUserIdAsync();
        request.UserConversationIds.Add(userId);
        IConversation conversation = await _conversationService.CreateConversationAsync(userId, request.GroupName, request.CreationTimeUnix, userId, request.IsOnline, request.LastMessageId, request.UserConversationIds);

        return Ok(conversation);
    }

    /// <summary>
    /// Update a conversation
    /// </summary>
    /// <response code="204">No Content.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Not Found.</response>
    [HttpPut("{conversationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateConversationAsync([FromRoute][Required] string conversationId) 
    {
        string userId = await _contextService.GetCurrentUserIdAsync();
        await _conversationService.UpdateConversationAsync(userId, conversationId);

        return NoContent();
    }

    /// <summary>
    /// Get user conversations
    /// </summary>
    /// <response code="200">Returns the conversation.</response>
    /// <response code="400">Bad Request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Not Found.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConversationAsync()
    {
        string userId = await _contextService.GetCurrentUserIdAsync();
        IReadOnlyList<IConversation> conversations = await _conversationService.GetConversationsAsync(userId);

        return Ok(conversations);
    }
}