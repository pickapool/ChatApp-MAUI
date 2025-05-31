using ChatApp_MAUI.Shared.Models;
using Firebase.Auth;
using FirebaseAdmin.Auth;

namespace ChatApp_MAUI.Shared.Services.RegistrationServices
{
    public class RegistrationService : IRegistrationService
    {
        FirebaseAuthClient _firebaseAuthClient;
        public RegistrationService(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }
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
        public async Task<string> MobileRegisterAsync(AccountModel account)
        {
            try
            {
                var response = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(account.email, account.password);
                var token = await response.User.GetIdTokenAsync();
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"Firebase error: {ex.Message}");
            }
        }
    }
}
