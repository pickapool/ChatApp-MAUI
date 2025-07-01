using ChatApp_MAUI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.CallBackServices.ChatRoomCallback
{
    public class ChatRoomCallbackService : IChatRoomCallbackService
    {
        private IChatRoomCallbackService? _listener;
        public async Task OnShowChatRoom()
        {
            if(_listener != null)
                await _listener.OnShowChatRoom();
        }

        public void RegisterCallback(IChatRoomCallbackService listener)
        {
            _listener = listener;
        }
    }
}
