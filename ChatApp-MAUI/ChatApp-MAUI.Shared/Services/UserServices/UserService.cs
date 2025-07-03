using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.UserServices
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
