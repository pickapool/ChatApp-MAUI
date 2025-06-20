using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Shared.Services.CustomAuthenticationServices
{
    public interface ILoginService
    {
        Task<string> Authenticate(UserRecordArgs args);
        Task<AuthTokenModel> GetUserRecord(string token);
    }
}
