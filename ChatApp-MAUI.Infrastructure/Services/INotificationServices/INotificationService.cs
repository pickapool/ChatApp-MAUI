using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure.Services.INotificationServices
{
    public interface INotificationService
    {
        HubConnection CreateNotificationBuilder();
    }
}
