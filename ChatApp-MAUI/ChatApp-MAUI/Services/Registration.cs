using ChatApp_MAUI.Models;
using ChatApp_MAUI.Shared.Services;
using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Services
{
    public class Registration : IRegistration
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
