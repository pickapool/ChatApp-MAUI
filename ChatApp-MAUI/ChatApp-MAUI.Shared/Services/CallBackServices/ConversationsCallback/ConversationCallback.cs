using ChatApp_MAUI.Shared.Models;
using Services.CallBackServices.LoadFriendsCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.CallBackServices.ConversationsCallback
{
    public class ConversationCallback : IConversationCallback
    {
        private IConversationCallback? _listener;
        public async Task OnShowConversation(AuthTokenModel user)
        {
            if (_listener != null)
                await _listener.OnShowConversation(user);
        }

        public void RegisterCallBack(IConversationCallback callback)
        {
            _listener = callback;
        }
        
    }
}
