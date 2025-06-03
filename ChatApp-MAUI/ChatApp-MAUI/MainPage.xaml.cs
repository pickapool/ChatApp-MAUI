using Microsoft.AspNetCore.Components.WebView;

namespace ChatApp_MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            blazorWebView.BlazorWebViewInitializing += BlazorWebViewInitializing;
            blazorWebView.BlazorWebViewInitialized += BlazorWebViewInitialized;
        }
        private void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e) { }
        private void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e) { }
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
