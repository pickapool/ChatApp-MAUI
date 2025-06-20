using Blazored.LocalStorage;
using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;
using System.Net.Http.Json;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;

namespace ChatApp_MAUI.Shared.Services.RegistrationServices
{
    public class RegistrationService : IRegistrationService
    {
        HttpClient _httpClient;
        public RegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{Extensions.GetHttpError(error)}");
                }
                response.EnsureSuccessStatusCode();
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Firebase error: {ex.Message}");
            }
        }
    }
}
