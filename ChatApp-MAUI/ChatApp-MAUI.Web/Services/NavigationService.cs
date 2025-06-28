using ChatApp_MAUI.Shared.Services.CameraServices;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Shared.Services.FirebaseStorageServices;
using ChatApp_MAUI.Shared.Services.NavigationServices;
using CommunityToolkit.Maui.Core;

namespace ChatApp_MAUI.Services
{
    public class NavigationService : INavigationService
    {
        public void GoToCameraPage(Page c, ICameraService cs)
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                Application.Current.MainPage?.Navigation?.PushAsync(c);
            });
        }
    }
}
