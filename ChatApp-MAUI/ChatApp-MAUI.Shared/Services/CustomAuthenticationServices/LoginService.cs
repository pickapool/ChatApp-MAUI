using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;
using System.Net.Http.Json;
using ChatApp_MAUI.Shared.Common;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;
using System.Net.Http.Headers;

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
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<AuthTokenModel> GetUserRecord(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/auth/getuser", token);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"{Extensions.GetHttpError(error)}");
            }
            var record = await response.Content.ReadFromJsonAsync<AuthTokenModel>();

            return record;
        }
    }
}
