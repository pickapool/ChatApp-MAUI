using ChatApp_MAUI.Shared.Models;

namespace ChatApp_MAUI.Shared.Services.RegistrationServices
{
    public interface IRegistrationService
    {
        Task<string> SignInAsync(AccountModel account);

        Task<string> RegisterAsync(AccountModel account);
    }
}
