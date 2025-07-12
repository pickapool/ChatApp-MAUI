namespace ChatApp_MAUI.Infrastructure.Services.CallBackServices.ChatRoomCallback
{
    public interface IChatRoomCallbackService
    {
        void RegisterCallback(IChatRoomCallbackService listener);
        Task OnShowChatRoom();
    }
}
