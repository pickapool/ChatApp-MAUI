using ChatApp_MAUI.Models;

namespace ChatApp_MAUI.Shared.Services
{
    public interface IRegistration
    {
        Task<string> RegisterAsync(AccountModel account);
    }
}
