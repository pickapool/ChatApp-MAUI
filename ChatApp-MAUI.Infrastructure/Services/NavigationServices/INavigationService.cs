using ChatApp_MAUI.Infrastructure.Services.CameraServices;

namespace ChatApp_MAUI.Infrastructure.Services.NavigationServices
{
    public interface INavigationService
    {
        void GoToCameraPage(Page c, ICameraService cs);
    }
}
