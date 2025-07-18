﻿using ChatApp_MAUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure.Services.MessageServices
{
    public interface IMessageService
    {
        Task<List<MessageModel>> GetMessages(FilterParameterModel param);
        Task<List<ChatRoomModel>> GetChatRooms(FilterParameterModel param);
        Task AddMessage(MessageModel param, string token);
        Task<string> GetChatRoomId(FilterParameterModel param);
    }
}
