namespace ChatApp_MAUI.Infrastructure.CallBackServices.LoadFriendsCallback
{
    public class CallBackService : ICallBackService
    {
        private ICallBackService? _listener;
        public void RegisterCallback(ICallBackService listener)
        {
            _listener = listener;
        }

        public async Task OnShowFrieds()
        {
            if (_listener != null)
                await _listener.OnShowFrieds();
        }
    }
}
