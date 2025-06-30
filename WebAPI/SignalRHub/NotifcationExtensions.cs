using ChatApp_MAUI.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalRHub
{
    public static class NotificationExtensions
    {
        public static async Task NotifyAsync(this IHubContext<NotificationHub> hubContext, FriendsModel model)
        {
            await hubContext.Clients.All.SendAsync("NotifyFriendRequest", model);
        }
    }
}
