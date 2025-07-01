using ChatApp_MAUI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.FriendServices
{
    public interface IFriendService
    {

        Task AccepFriendRequest(FriendsModel model, string token);
        Task<List<FriendsModel>> GetFriends(string uid, string token);
    }
}
