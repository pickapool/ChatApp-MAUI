using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Services.CameraServices;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Shared.Services.FirebaseStorageServices;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp_MAUI.Components;

public partial class CameraViewPage : ContentPage
{
    private readonly ICameraProvider _cameraProvider;
    private readonly ICameraService _cameraService;
    bool isBack = true;
	public CameraViewPage(ICameraProvider cameraProvider, ICameraService cameraService)
	{
		InitializeComponent();
        _cameraService = cameraService;
        _cameraProvider = cameraProvider;
    }

    private async void OnMediaCapture(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
        if (Dispatcher.IsDispatchRequired)
        {
            try
            {
                await _cameraService.OnCapture(e.Media);

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Navigation.PopAsync();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnMediaCapture: {ex.Message}");
            }
        }
        //Photo.Source = ImageSource.FromStream(() => e.Media);
    }
    private void OnCaptureClicked(object sender, EventArgs e)
    {
        cameraView.CaptureImage(CancellationToken.None);
    }
    private async void BackCamera(object sender, EventArgs e)
    {
        await _cameraProvider.RefreshAvailableCameras(CancellationToken.None);
        if(isBack)
            cameraView.SelectedCamera = _cameraProvider.AvailableCameras.Where(c => c.Position == CameraPosition.Front).FirstOrDefault();
        else
            cameraView.SelectedCamera = _cameraProvider.AvailableCameras.Where(c => c.Position == CameraPosition.Rear).FirstOrDefault();
        isBack = !isBack;
    }
    private async void OnBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}