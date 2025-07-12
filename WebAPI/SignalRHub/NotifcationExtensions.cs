using ChatApp_MAUI.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalRHub
{
    public static class NotificationExtensions
    {
        public static async Task NotifyAsync(this IHubContext<NotificationHub> hubContext, FriendsModel model)
        {
            await hubContext.Clients.All.SendAsync("NotifyFriendRequest", model);
        }
        public static async Task NotifyMessageAsync(this IHubContext<NotificationHub> hubContext, MessageModel model)
        {
            await hubContext.Clients.All.SendAsync("NotifyMessage", model);
        }
    }
}
