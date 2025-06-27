using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.FirebaseStorageServices
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadPhoto(Stream stream, string fileName, string folderName);
    }
}
