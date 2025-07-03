using ChatApp_MAUI.Domain.Entities;
using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Shared.Services.RegistrationServices
{
    public interface IRegistrationService
    {
        Task RegisterAsync(UserRecordArgs args);
    }
}
