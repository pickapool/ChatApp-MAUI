﻿using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Dialogs;
using ChatApp_MAUI.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using SkiaSharp;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;
using Severity = MudBlazor.Severity;
using ChatApp_MAUI.Infrastructure.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Infrastructure.Services.FirebaseStorageServices;
using ChatApp_MAUI.Infrastructure.Services;
using ChatApp_MAUI.Infrastructure.Services.NavigationServices;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.CameraServices;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class ProfilePageBase : ComponentBase, ICameraService
    {
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] protected IFirebaseStorageService _firebaseStorageService { get; set; } = default!;
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        [Inject] protected IDialogService _dialogService { get; set; } = default!;
        [Inject] protected INavigationService _navigationService { get; set; } = default!;
        [Inject] protected IFormFactor _formFactor { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;

        protected IBrowserFile? selectedFile;
        protected bool isUploading = false, isLoading = false;
        protected string code = string.Empty;
        protected  FluentValueValidator<string> ccValidator = new FluentValueValidator<string>(x => x.NotEmpty().MinimumLength(10).MaximumLength(10));
        private string factor => _formFactor.GetFormFactor();
        private string platform => _formFactor.GetPlatform();
        protected override void OnInitialized()
        {
            if(!String.IsNullOrEmpty(_appStateService.User.PhoneNumber))
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
                    await UploadProfile(memoryStream);
                }
            }
            //TODO upload the files to the server
        }
        private async Task UploadProfile(Stream stream)
        {
            await selectedFile.OpenReadStream(209715200).CopyToAsync(stream);
            stream.Position = 0;
            var url = await _firebaseStorageService.UploadPhoto(stream, _appStateService.User.Uid, "ProfilePicture");
            _appStateService.User.PhotoUrl = url;
            await _loginService.UpdatePhoto(_appStateService.Token, _appStateService.User);
            isUploading = false;
            SnackBarHelper.ShowSnackbar("Profile picture has been uploaded", Variant.Filled, _snackBar, Severity.Success);
            _notifierService.NotifyChanged();
            StateHasChanged();
        }
        protected async Task GetVerificationEmailLink()
        {
            var response = await _loginService.GetVerificationLink(_appStateService.Token, _appStateService.User.Email);
            await _jsRuntime.InvokeVoidAsync("open", response, "_blank");
        }
        protected async Task UpdateProfile()
        {
            isLoading = true;
            try
            {
                if (!ccValidator.Validate(_appStateService.User.PhoneNumber).IsValid)
                {
                    SnackBarHelper.ShowSnackbar("Please enter a valid phone number", Variant.Filled, _snackBar, Severity.Error);
                    return;
                }
                AuthTokenModel record = _appStateService.User.Clone();
                record.PhoneNumber = String.Format("+{0}{1}", code, record.PhoneNumber);
                await _loginService.UpdateProfile(_appStateService.Token, record);
                SnackBarHelper.ShowSnackbar("Profile successfully saved.", Variant.Filled, _snackBar, Severity.Success);
            } catch( Exception ee )
            {
                SnackBarHelper.ShowSnackbar(ee.Message, Variant.Filled, _snackBar, Severity.Error);
            } finally
            {
                isLoading = false;
            }
        }
        private void FormatCodeAndPhone()
        {
            code = _appStateService.User.PhoneNumber.Clone().ToString()?.Substring(1, 2) ?? string.Empty;
            _appStateService.User.PhoneNumber = _appStateService.User.PhoneNumber.Remove(0, 3);
        }
        protected async Task OpenCameraDialog()
        {
            if (platform.ToLower().Contains("android") || platform.ToLower().Contains("ios"))
            {
                _navigationService.GoToCameraPage(new Microsoft.Maui.Controls.Page(), this);
            }
            else
            {
                var parameters = new DialogParameters();
                parameters.Add("_cameraInterface", this);
                var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium, BackdropClick = false };
                var resultDialog = await _dialogService.ShowAsync<CameraDialog>("Delete", parameters, options);
                if (resultDialog != null)
                {
                    // Handle dialog result if necessary
                }
            }
        }

        public async Task OnCapture(Stream stream)
        {
            await InvokeAsync(async () =>
            {
                isUploading = true;
                StateHasChanged();
                var url = await _firebaseStorageService.UploadPhoto(stream, _appStateService.User.Uid, "ProfilePicture");
                _appStateService.User.PhotoUrl = url;

                isUploading = false;
                SnackBarHelper.ShowSnackbar("Profile picture has been uploaded", Variant.Filled, _snackBar, Severity.Success);
                _notifierService.NotifyChanged();
                StateHasChanged();
            });
        }
    } 
}
