using ChatApp_MAUI.Shared.Services.NavigationServices;
using CommunityToolkit.Maui.Core;

namespace ChatApp_MAUI.Services
{
    public class NavigationService : INavigationService
    {
        public void GoToCameraPage(Page c)
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                Application.Current.MainPage?.Navigation?.PushAsync(c);
            });
        }
    }
}
