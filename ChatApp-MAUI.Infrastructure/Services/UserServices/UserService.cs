using ChatApp_MAUI.Domain.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ChatApp_MAUI.Infrastructure.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AuthTokenModel>> SearchUsers(FilterParameterModel param)
        {
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", param.Token?.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/auth/searchuser", param);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception($"Error fetching users: {error}");
            }
            return await response.Content.ReadFromJsonAsync<List<AuthTokenModel>>();
        }
        public async Task<string> SendFriendRequest(FriendsModel model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
            var response = await  _httpClient.PostAsJsonAsync("api/friends/addfriend", model);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<AuthTokenModel> GetUserAccount(FilterParameterModel param)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", param.Token?.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/auth/getuseraccount", param);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
            return await response.Content.ReadFromJsonAsync<AuthTokenModel>();
        }
    }
}
