using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Shared.Services.RegistrationServices
{
    public interface IRegistrationService
    {
        Task<string> SignInAsync(AccountModel account);

        Task RegisterAsync(UserRecordArgs args);
    }
}
