using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.CameraServices
{
    public interface ICameraService
    {
        Task OnCapture(Stream stream);
    }
}
