using Microsoft.AspNetCore.Components.WebView;

namespace ChatApp_MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            //Permission request
            var cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
            var micStatus = await Permissions.RequestAsync<Permissions.Microphone>();

            if (cameraStatus != PermissionStatus.Granted || micStatus != PermissionStatus.Granted)
            {
                await DisplayAlert("Permissions Required", "Camera and microphone access is needed.", "OK");
            }
        }
    }
}
