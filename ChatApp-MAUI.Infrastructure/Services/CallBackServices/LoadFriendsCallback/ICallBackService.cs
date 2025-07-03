
namespace ChatApp_MAUI.Infrastructure.CallBackServices.LoadFriendsCallback
{
    public interface ICallBackService
    {
        Task OnShowFrieds();
        void RegisterCallback(ICallBackService listener);

    }
}
