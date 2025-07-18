﻿namespace ChatApp_MAUI.Infrastructure.Services.CallBackServices.ChatRoomCallback
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
