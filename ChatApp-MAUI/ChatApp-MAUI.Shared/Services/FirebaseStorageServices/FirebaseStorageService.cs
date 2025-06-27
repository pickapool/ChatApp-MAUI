using ChatApp_MAUI.Shared.Common;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.FirebaseStorageServices
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        public async Task<string> UploadPhoto(Stream stream, string fileName, string folderName)
        {
            var task = new FirebaseStorage("maui-5e9d0.firebasestorage.app",
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(GlobalClass.Token),
                    ThrowOnCancel = true
                })
                .Child(folderName)
                .Child(fileName)
                .PutAsync(stream);  
            try
            {
                var downloadUrl = await task;
                return downloadUrl;
            } catch(Exception ee)
            {
                throw;
            }
            
        }
    }
}
