using BlazorCameraStreamer;
using ChatApp_MAUI.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class CameraComponentBase : ComponentBase
    {
        protected CameraStreamer? CameraStreamerReference;
        protected string? cameraId;
        [Parameter] public EventCallback<Stream> OnCapture { get; set; }
        //Bitmap? bmp;
        protected async void OnRenderedHandler()
        {

            try
            {
                if (await CameraStreamerReference?.GetCameraAccessAsync())
                {
                    await CameraStreamerReference.ReloadAsync();
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

        protected async Task OnFrameHandler(string data)
        {
            data = data[(data.IndexOf(',') + 1)..];
            Bitmap bmp = new(new MemoryStream(Convert.FromBase64String(data)));
            await OnCapture.InvokeAsync(new MemoryStream(Convert.FromBase64String(data)));
        }
    }
}
