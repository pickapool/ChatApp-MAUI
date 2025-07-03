using ChatApp_MAUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure.Services.FriendServices
{
    public class FriendService : IFriendService
    {

        private readonly HttpClient _httpClient;
        public FriendService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task AccepFriendRequest(FriendsModel model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/friends/acceptfriend", model);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
        }

        public async Task<List<FriendsModel>> GetFriends(string uid, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/friends/getfriends", uid);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
            return await response.Content.ReadFromJsonAsync<List<FriendsModel>>();
        }
    }
}
