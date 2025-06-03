using ChatApp_MAUI.Components;
using ChatApp_MAUI.Shared.Services.NavigationServices;
using CommunityToolkit.Maui.Core;

namespace ChatApp_MAUI.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ICameraProvider cameraProvider;
        public NavigationService(ICameraProvider _c) {
            cameraProvider = _c;
        }
        public void GoToCameraPage()
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                Application.Current.MainPage?.Navigation?.PushAsync(new CameraViewPage(cameraProvider));
            });
        }
    }
}
