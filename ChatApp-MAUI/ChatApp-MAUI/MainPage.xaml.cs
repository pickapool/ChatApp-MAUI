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
            var storageRead = await Permissions.RequestAsync<Permissions.StorageRead>();
            var storageWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (cameraStatus != PermissionStatus.Granted || micStatus != PermissionStatus.Granted ||
                storageRead != PermissionStatus.Granted || storageWrite != PermissionStatus.Granted)
            {
                await DisplayAlert("Permissions Required", "Camera and microphone access is needed.", "OK");
            }
        }
    }
}
