using ChatApp_MAUI.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalRHub
{
    public class NotificationHub : Hub
    {
        public async Task NotifyAsync(FriendsModel model)
        {
            await Clients.All.SendAsync("NotifyFriendRequest", model);
        }
    }
}
