using ChatApp_MAUI.Domain.Entities;
using FirebaseAdmin.Auth;
using MediatR;

namespace WebAPI.Commands.FriendRequestCommands.GetCommand
{
    public record class GetFriendRequestCommand(string uid) : IRequest<IEnumerable<FriendsModel>>;
}
