using ChatApp_MAUI.Infrastructure.Services.CameraServices;
using ChatApp_MAUI.Infrastructure.Services.NavigationServices;

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
