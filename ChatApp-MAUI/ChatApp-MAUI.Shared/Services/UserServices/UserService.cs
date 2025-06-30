using ChatApp_MAUI.Shared.Models;
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

        public Task<List<AuthTokenModel>> SearchUsers(FilterParameterModel param)
        {
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", param.Token?.Trim('"'));
            var response = _httpClient.PostAsJsonAsync("api/auth/searchuser", param);
            if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Result.Content.ReadAsStringAsync().Result;
                throw new Exception($"Error fetching users: {error}");
            }
            return response.Result.Content.ReadFromJsonAsync<List<AuthTokenModel>>();
        }
    }
}
