using ChatApp_MAUI.Domain.Entities;

namespace ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback
{
    public class ConversationCallback : IConversationCallback
    {
        private readonly List<IConversationCallback> _listeners = new();

        public async Task OnShowConversation(AuthTokenModel user)
        {
            foreach (var listener in _listeners)
            {
                await listener.OnShowConversation(user);
            }
        }

        public void RegisterCallBack(IConversationCallback callback)
        {
            if (!_listeners.Contains(callback))
                _listeners.Add(callback);
        }
        
    }
}
