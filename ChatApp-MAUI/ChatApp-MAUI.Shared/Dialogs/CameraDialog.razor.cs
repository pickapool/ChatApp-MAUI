using ChatApp_MAUI.Infrastructure.Services;
using ChatApp_MAUI.Infrastructure.Services.CameraServices;
using ChatApp_MAUI.Infrastructure.Services.NavigationServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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
