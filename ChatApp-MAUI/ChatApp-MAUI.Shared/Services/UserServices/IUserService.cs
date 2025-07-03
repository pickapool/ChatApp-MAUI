using ChatApp_MAUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.UserServices
{
    public interface IUserService
    {
        Task<List<AuthTokenModel>> SearchUsers(FilterParameterModel param);
        Task<string> SendFriendRequest(FriendsModel model, string token);
        Task<AuthTokenModel> GetUserAccount(FilterParameterModel param);
    }
}
