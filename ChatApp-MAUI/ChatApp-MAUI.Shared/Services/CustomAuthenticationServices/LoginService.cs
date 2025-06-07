using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;
using System.Net.Http.Json;
using ChatApp_MAUI.Shared.Common;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;

namespace ChatApp_MAUI.Shared.Services.CustomAuthenticationServices
{
    public class LoginService : ILoginService
    {
        HttpClient _httpClient;
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Authenticate(UserRecordArgs args)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", args);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"{Extensions.GetHttpError(error)}");
            }
            response.EnsureSuccessStatusCode();
            var authToken = await response.Content.ReadAsStringAsync();
            return authToken;
        }
    }
}
