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
        public async Task<string> SignInAsync(AccountModel account)
        {
            try
            {
                var response = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(account.email, account.password);
                var token = await response.User.GetIdTokenAsync();
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"Firebase error: {ex.Message}");
            }
        }
        public async Task<string> RegisterAsync(AccountModel account)
        {
            if (string.IsNullOrEmpty(account.email) || string.IsNullOrEmpty(account.password))
            {
                throw new ArgumentException("Email and password is required.");
            }
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
