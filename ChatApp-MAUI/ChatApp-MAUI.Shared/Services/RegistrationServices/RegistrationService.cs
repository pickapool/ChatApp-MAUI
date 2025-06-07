using Blazored.LocalStorage;
using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;
using System.Net.Http.Json;

namespace ChatApp_MAUI.Shared.Services.RegistrationServices
{
    public class RegistrationService : IRegistrationService
    {
        ILocalStorageService _localStorage;
        HttpClient _httpClient;
        public RegistrationService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;
            _httpClient = httpClient;
        }
        public async Task<string> SignInAsync(AccountModel account)
        {
            try
            {
               // var response = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(account.email, account.password);
                //var token = await response.User.GetIdTokenAsync();
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception($"Firebase error: {ex.Message}");
            }
        }
        public async Task RegisterAsync(UserRecordArgs args)
        {
            if (string.IsNullOrEmpty(args.Email) || string.IsNullOrEmpty(args.Password) || string.IsNullOrEmpty(args.DisplayName))
            {
                throw new ArgumentException("All fields are required.");
            }
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", args);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"Firebase error: {ex.Message}");
            }
        }
    }
}
