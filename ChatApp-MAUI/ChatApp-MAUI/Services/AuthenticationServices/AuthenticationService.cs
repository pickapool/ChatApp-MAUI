using ChatApp_MAUI.Models;
using FirebaseAdmin.Auth;
using System.Net.Http.Json;

namespace ChatApp_MAUI.Services.AuthenticationServices
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        HttpClient _httpClient;
        public AuthenticationService(HttpClient httpClient)
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
            return authToken.IdToken;
        }
    }
}
