using ChatApp_MAUI.Shared.Models;
namespace ChatApp_MAUI.Shared.Services.CallBackServices.ChatRoomCallback
{
    public interface IChatRoomCallbackService
    {
        void RegisterCallback(IChatRoomCallbackService listener);
        Task OnShowChatRoom();
    }
}
