using ChatApp_MAUI.Models;
using FirebaseAdmin.Auth;
using MediatR;

namespace ChatApp_MAUI.Services.AccountRegistrationServices
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
