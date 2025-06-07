using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Shared.Services.CustomAuthenticationServices
{
    public interface ILoginService
    {
        Task<string> Authenticate(UserRecordArgs args);
    }
}
