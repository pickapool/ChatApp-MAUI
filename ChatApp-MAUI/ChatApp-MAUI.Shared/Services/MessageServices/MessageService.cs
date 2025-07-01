using ChatApp_MAUI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MessageModel>> GetMessages(FilterParameterModel param)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", param.Token.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/message/getconversation", param);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
            return await response.Content.ReadFromJsonAsync<List<MessageModel>>();
        }
        public async Task<List<ChatRoomModel>> GetChatRooms(FilterParameterModel param)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", param.Token.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/message/getchatroom", param);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
            return await response.Content.ReadFromJsonAsync<List<ChatRoomModel>>();
        }

        public async Task AddMessage(MessageModel param, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/message/addmessage", param);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
        }
        public async Task<string> GetChatRoomId(FilterParameterModel param)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", param.Token.Trim('"'));
            var response = await _httpClient.PostAsJsonAsync("api/message/getchatroomid", param);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception(error);
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
