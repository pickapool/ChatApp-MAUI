using ChatApp_MAUI.Domain.Entities;
using FirebaseAdmin.Auth;
using MediatR;

namespace WebAPI.Commands.FriendRequestCommands.DeletCommand
{
    public record class DeleteFriendRequestCommand(string documentID) : IRequest<int>;
}
