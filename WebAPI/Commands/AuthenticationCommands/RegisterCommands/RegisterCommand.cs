using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Auth;
using MediatR;

namespace WebAPI.Commands.AuthenticationCommands.RegisterCommands
{
    public record class RegisterCommand(AuthTokenModel? AuthTokenModel) : IRequest<UserRecord>;
}
