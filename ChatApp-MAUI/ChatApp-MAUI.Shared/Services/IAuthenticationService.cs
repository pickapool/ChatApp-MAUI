namespace ChatApp_MAUI.Shared
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(string email, string password);
    }
}
