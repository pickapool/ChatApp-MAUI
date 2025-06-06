namespace ChatApp_MAUI.Shared.Services.CustomAuthenticationServices
{
    public interface ILoginService
    {
        Task<string> Authenticate(string email, string password);
    }
}
