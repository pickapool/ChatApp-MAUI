using ChatApp_MAUI.Domain.Entities;
namespace ChatApp_MAUI.Shared.Services.CallBackServices.ChatRoomCallback
{
    public interface IChatRoomCallbackService
    {
        void RegisterCallback(IChatRoomCallbackService listener);
        Task OnShowChatRoom();
    }
}
