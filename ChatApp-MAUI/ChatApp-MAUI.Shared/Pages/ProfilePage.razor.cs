using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Shared.Services.FirebaseStorageServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class ProfilePageBase : ComponentBase
    {
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] protected IFirebaseStorageService _firebaseStorageService { get; set; } = default!;
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        protected IBrowserFile? selectedFile;
        protected bool isUploading = false;
        protected override async Task OnInitializedAsync()
        {

        }
        protected async Task UploadFiles(IBrowserFile file)
        {
            isUploading = true;
            selectedFile = file;
            if (selectedFile != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await selectedFile.OpenReadStream(209715200).CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    var url = await _firebaseStorageService.UploadPhoto(memoryStream, selectedFile.Name, "ProfilePicture");
                    GlobalClass.User.PhotoUrl = url;
                    await _loginService.UpdateProfile(GlobalClass.Token, GlobalClass.User);
                    isUploading = false;
                    Extensions.ShowSnackbar("Profile picture has been uploaded", Variant.Filled, _snackBar, Severity.Success);
                    StateHasChanged();
                }
            }
            //TODO upload the files to the server
        }
        protected async Task GetVerificationEmailLink()
        {
            var response = await _loginService.GetVerificationLink(GlobalClass.Token, GlobalClass.User.Email);
            await _jsRuntime.InvokeVoidAsync("open", response, "_blank");
        }
    }
}
