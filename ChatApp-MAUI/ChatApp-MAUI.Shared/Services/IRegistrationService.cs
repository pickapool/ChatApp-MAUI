
using ChatApp_MAUI.Shared.Models;

namespace ChatApp_MAUI.Shared.Services
{
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(AccountModel account);
    }
}
