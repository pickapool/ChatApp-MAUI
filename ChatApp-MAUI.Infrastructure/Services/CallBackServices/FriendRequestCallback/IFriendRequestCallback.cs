using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ChatRoomCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure.Services.CallBackServices.FriendRequestCallback
{
    public interface IFriendRequestCallback
    {
        void RegisterCallback(IFriendRequestCallback listener);
        Task OnShowRequest(AppStateService app);
    }
}
