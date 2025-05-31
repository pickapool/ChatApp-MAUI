namespace ChatApp_MAUI.Shared.Services.CustomAuthenticationServices
{
    public interface ICustomAuthenticationService
    {
        Task<string> Authenticate(string email, string password);
    }
}
