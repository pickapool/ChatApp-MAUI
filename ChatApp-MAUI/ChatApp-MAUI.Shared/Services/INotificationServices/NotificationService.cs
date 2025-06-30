using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.INotificationServices { 
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public HubConnection CreateNotificationBuilder()
        {
            HubConnection hubConnection = new HubConnectionBuilder()
            .WithUrl($"{_configuration["BaseAPI:Url"]}/NotificationHub")
            .WithAutomaticReconnect()
            .Build();

            return hubConnection;
        }
    }
}
