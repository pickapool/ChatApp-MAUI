using BlazorCameraStreamer;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class CameraComponentBase : ComponentBase
    {
        protected CameraStreamer CameraStreamerReference;
        protected string cameraId = null;

        protected async void OnRenderedHandler()
        {

            try
            {
                if (await CameraStreamerReference.GetCameraAccessAsync())
                {
                    await CameraStreamerReference.ReloadAsync();
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

        protected void OnFrameHandler(string data)
        {
            // Remove the suffix added by javascript
            data = data[(data.IndexOf(',') + 1)..];

            // Convert the base64 string to a System.Drawing.Bitmap
            Bitmap bmp = new(new MemoryStream(Convert.FromBase64String(data)));

            // Do something with the bitmap
        }
    }
}
