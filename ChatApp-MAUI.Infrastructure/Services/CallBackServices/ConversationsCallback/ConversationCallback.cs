using ChatApp_MAUI.Domain.Entities;

namespace ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback
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
