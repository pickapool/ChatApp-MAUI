using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Shared.Services.CustomAuthenticationServices
{
    public interface ILoginService
    {
        Task<string> Authenticate(UserRecordArgs args);
        Task<AuthTokenModel> GetUserRecord(string token);
        Task<string> GetVerificationLink(string token, string email);
        Task<AuthTokenModel> UpdateProfile(string token, AuthTokenModel model);
        Task<AuthTokenModel> UpdatePhoto(string token, AuthTokenModel model);
    }
}
