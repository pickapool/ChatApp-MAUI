using ChatApp_MAUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure.Services.FriendServices
{
    public interface IFriendService
    {
        Task AccepFriendRequest(FriendsModel model, string token);
        Task<List<FriendsModel>> GetFriends(string uid, string token);
        Task<IEnumerable<FriendsModel>> GetFriendRequest(string uid, string token);
        Task DeleteFriendRequest(string documentKey, string token);
    }
}
