using ChatApp_MAUI.Shared.Models;

namespace ChatApp_MAUI.Shared.Services.RegistrationServices
{
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(AccountModel account);

        Task<string> MobileRegisterAsync(AccountModel account);
    }
}
