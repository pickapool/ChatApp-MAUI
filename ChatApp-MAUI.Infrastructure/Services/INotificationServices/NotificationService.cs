using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure.Services.NotificationServices { 
    public static class NotificationService
    {
        public static HubConnection GetConnetion(string connection)
        {
            HubConnection hubConnection = new HubConnectionBuilder()
            .WithUrl(connection)
            .WithAutomaticReconnect()
            .Build();

            return hubConnection;
        }
    }
}
