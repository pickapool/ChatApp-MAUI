using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ChatRoomCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure.Services.CallBackServices.FriendRequestCallback
{
    public class FriendRequestCallback : IFriendRequestCallback
    {
        private IFriendRequestCallback? _listener;
        public async Task OnShowRequest(AppStateService app)
        {
            if (_listener != null)
                await _listener.OnShowRequest(app);
        }

        public void RegisterCallback(IFriendRequestCallback listener)
        {
            _listener = listener;
        }
    }
}
