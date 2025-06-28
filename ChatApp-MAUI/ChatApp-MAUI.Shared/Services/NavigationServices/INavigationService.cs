using ChatApp_MAUI.Shared.Services.CameraServices;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Shared.Services.FirebaseStorageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.NavigationServices
{
    public interface INavigationService
    {
        void GoToCameraPage(Page c, ICameraService cs);
    }
}
