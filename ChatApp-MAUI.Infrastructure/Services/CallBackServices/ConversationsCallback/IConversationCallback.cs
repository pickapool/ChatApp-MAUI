using ChatApp_MAUI.Domain.Entities;

namespace ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback
{
    public interface IConversationCallback
    {
        void RegisterCallBack(IConversationCallback callback);
        Task OnShowConversation(AuthTokenModel user);
    }
}
