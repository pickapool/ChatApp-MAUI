using ChatApp_MAUI.Components;
using ChatApp_MAUI.Infrastructure.Services.CameraServices;
using ChatApp_MAUI.Infrastructure.Services.NavigationServices;
using CommunityToolkit.Maui.Core;

namespace ChatApp_MAUI.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ICameraProvider cameraProvider;
        public NavigationService(ICameraProvider _c)
        {
            cameraProvider = _c;
        }
        public void GoToCameraPage(Page c, ICameraService cs)
        {
            c = new CameraViewPage(cameraProvider, cs);
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                mainPage?.Navigation?.PushAsync(c);
            });
        }
    }
}
