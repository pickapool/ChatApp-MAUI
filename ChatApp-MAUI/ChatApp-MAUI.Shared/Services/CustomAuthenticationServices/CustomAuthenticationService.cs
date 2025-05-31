using ChatApp_MAUI.Shared.Models;
using System.Net.Http.Json;

namespace ChatApp_MAUI.Shared.Services.CustomAuthenticationServices
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        HttpClient _httpClient;
        public CustomAuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Authenticate(string email, string password)
        {
            var request = new
            {
                email,
                password,
                returnSecureToken = true
            };
            var response = await _httpClient.PostAsJsonAsync("", request);
            var authToken = await response.Content.ReadFromJsonAsync<AuthTokenModel>();
            return authToken?.IdToken ?? string.Empty;
        }
    }
}
