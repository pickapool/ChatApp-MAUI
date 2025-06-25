using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Shared.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class CameraPageBase : ComponentBase
    {
        //Service
        [Inject] protected INavigationService _navigationService { get; set; } = default!;
        [Inject] protected IFormFactor _formFactor { get; set; } = default!;
        protected bool isAndroid = false;
        protected override void OnInitialized()
        {
            if (platform.ToLower().Contains("android") || platform.ToLower().Contains("ios"))
            {
                isAndroid = true;
                _navigationService.GoToCameraPage(new Microsoft.Maui.Controls.Page());
            }
            else
            {
                isAndroid = false;
            }
        }
        private string factor => _formFactor.GetFormFactor();
        private string platform => _formFactor.GetPlatform();
    }
}
