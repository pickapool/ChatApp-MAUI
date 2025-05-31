using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services;
using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Services
{
    public class RegistrationService : IRegistrationService
    {
        public async Task<string> RegisterAsync(AccountModel account)
        {
            var userArg = new UserRecordArgs
            {
                Email = account.email,
                Password = account.password,
            };
            var response = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArg);

            return response.Uid;
        }
    }
}
