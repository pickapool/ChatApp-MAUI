using ChatApp_MAUI.Infrastructure;
using Firebase.Storage;
using Microsoft.AspNetCore.Components;

namespace ChatApp_MAUI.Infrastructure.Services.FirebaseStorageServices
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        [Inject] private AppStateService _appStateService { get; set; } = default!;
        public async Task<string> UploadPhoto(Stream stream, string fileName, string folderName)
        {
            var task = new FirebaseStorage("maui-5e9d0.firebasestorage.app",
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(_appStateService.Token),
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
                Console.WriteLine(ee.Message);
                throw;
            }
            
        }
    }
}
