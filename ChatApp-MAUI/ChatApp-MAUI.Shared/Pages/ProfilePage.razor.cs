using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Shared.Services.FirebaseStorageServices;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;
using Severity = MudBlazor.Severity;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class ProfilePageBase : ComponentBase
    {
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] protected IFirebaseStorageService _firebaseStorageService { get; set; } = default!;
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        protected IBrowserFile? selectedFile;
        protected bool isUploading = false, isLoading = false;
        protected string code = string.Empty;
        protected  FluentValueValidator<string> ccValidator = new FluentValueValidator<string>(x => x.NotEmpty().MinimumLength(10).MaximumLength(10));
        protected override void OnInitialized()
        {
            if(!String.IsNullOrEmpty(GlobalClass.User.PhoneNumber))
            {
                FormatCodeAndPhone();
            }
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
                    _notifierService.NotifyChanged();
                }
            }
            //TODO upload the files to the server
        }
        protected async Task GetVerificationEmailLink()
        {
            var response = await _loginService.GetVerificationLink(GlobalClass.Token, GlobalClass.User.Email);
            await _jsRuntime.InvokeVoidAsync("open", response, "_blank");
        }
        protected async Task UpdateProfile()
        {
            isLoading = true;
            try
            {
                if (!ccValidator.Validate(GlobalClass.User.PhoneNumber).IsValid)
                {
                    Extensions.ShowSnackbar("Please enter a valid phone number", Variant.Filled, _snackBar, Severity.Error);
                    return;
                }
                
                GlobalClass.User.PhoneNumber = String.Format("+{0}{1}", code, GlobalClass.User.PhoneNumber);
                await _loginService.UpdateProfile(GlobalClass.Token, GlobalClass.User);
                Extensions.ShowSnackbar("Profile successfully saved.", Variant.Filled, _snackBar, Severity.Success);
                FormatCodeAndPhone();
            } catch( Exception ee )
            {
                Extensions.ShowSnackbar(ee.Message, Variant.Filled, _snackBar, Severity.Error);
            } finally
            {
                isLoading = false;
            }
        }
        private void FormatCodeAndPhone()
        {
            code = GlobalClass.User.PhoneNumber.Clone().ToString()?.Substring(1, 2) ?? string.Empty;
            GlobalClass.User.PhoneNumber = GlobalClass.User.PhoneNumber.Remove(0, 3);
        }
    }
}
