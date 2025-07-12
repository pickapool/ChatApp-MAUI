namespace ChatApp_MAUI.Infrastructure.Services.CameraServices
{
    public interface ICameraService
    {
        Task OnCapture(Stream stream);
    }
}
