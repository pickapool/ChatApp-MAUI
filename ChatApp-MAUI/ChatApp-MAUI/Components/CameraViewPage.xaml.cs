using ChatApp_MAUI.Shared.Pages;
using ChatApp_MAUI.Shared.Services.NavigationServices;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using MudBlazor;

namespace ChatApp_MAUI.Components;

public partial class CameraViewPage : ContentPage
{
    private readonly ICameraProvider cameraProvider;
    bool isBack = true;
	public CameraViewPage(ICameraProvider _c)
	{
		InitializeComponent();
        cameraProvider = _c;
    }

    private void OnMediaCapture(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
		if(Dispatcher.IsDispatchRequired)
		{
			Dispatcher.Dispatch(() => Photo.Source = ImageSource.FromStream(()=> e.Media));
            return;
        }
		Photo.Source = ImageSource.FromStream(() => e.Media);
    }
	private void OnCaptureClicked(object sender, EventArgs e)
    {
        cameraView.CaptureImage(CancellationToken.None);
    }
    private async void BackCamera(object sender, EventArgs e)
    {
        await cameraProvider.RefreshAvailableCameras(CancellationToken.None);
        if(isBack)
            cameraView.SelectedCamera = cameraProvider.AvailableCameras.Where(c => c.Position == CameraPosition.Front).FirstOrDefault();
        else
            cameraView.SelectedCamera = cameraProvider.AvailableCameras.Where(c => c.Position == CameraPosition.Rear).FirstOrDefault();
        isBack = !isBack;
    }
    private void OnBack(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}