using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ChatApp_MAUI.Shared.Services.CameraServices;

namespace ChatApp_MAUI.Shared.Dialogs
{
    public partial class CameraDialogBase : ComponentBase
    {
        [Inject] protected INavigationService _navigationService { get; set; } = default!;
        [Inject] protected IFormFactor _formFactor { get; set; } = default!;
        [CascadingParameter] IMudDialogInstance? MudDialog { get; set; }
        [Parameter] public ICameraService _cameraInterface { get; set; } = default!;
        protected bool isAndroid = false;
        private Stream? stream;
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
        protected void Capture(Stream s)
        {
            stream = s;
        }
        protected void Submit()
        {
            if(stream != null)
                _cameraInterface.OnCapture(stream);
            MudDialog?.Close(DialogResult.Ok(true));
        }
        private string factor => _formFactor.GetFormFactor();
        private string platform => _formFactor.GetPlatform();
        protected void Cancel() => MudDialog?.Cancel();
    }
}
