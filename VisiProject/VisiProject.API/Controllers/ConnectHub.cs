using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using VisiProject.Contracts.Services;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ConnectHub : Hub
{
    private readonly IUserService _userService;
    private readonly IContextService _contextService;

    public ConnectHub(IUserService userService, IContextService contextService)
    {
        _userService = userService;
        _contextService = contextService;
    }

    public async Task AddToGroupConnection()
    {
        string userId = await _contextService.GetCurrentUserIdAsync();
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        await _userService.ActiveUserAsync(userId);

        await Clients.All.SendAsync("UserOnlineStatusChanged", userId, true);
    }

    public async Task RemoveFromGroupConnection()
    {
        string userId = await _contextService.GetCurrentUserIdAsync();
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        await _userService.InactiveUserAsync(userId);

        await Clients.All.SendAsync("UserOnlineStatusChanged", userId, false);
    }

    public async Task SendMessageToGroupConnection(string userId, string message)
    {
        await Clients.Group(userId).SendAsync("ReceiveMessageConnection", message);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await RemoveFromGroupConnection();
        await base.OnDisconnectedAsync(exception);
    }
}