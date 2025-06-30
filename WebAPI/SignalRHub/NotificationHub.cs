using ChatApp_MAUI.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalRHub
{
    public class NotificationHub : Hub
    {
        public async Task Notify(FriendsModel model)
        {
            // Send the message to all connected clients
            await Clients.All.SendAsync("NotifyFriendRequest", model);
        }
    }
}
