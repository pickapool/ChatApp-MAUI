using Microsoft.AspNetCore.SignalR.Client;
namespace ChatApp_MAUI.Infrastructure.Services.INotificationServices { 
    public static class NotificationService
    {
        public static HubConnection GetConnection(string connection)
        {
            HubConnection hubConnection = new HubConnectionBuilder()
            .WithUrl(connection)
            .WithAutomaticReconnect()
            .Build();

            return hubConnection;
        }
    }
}
