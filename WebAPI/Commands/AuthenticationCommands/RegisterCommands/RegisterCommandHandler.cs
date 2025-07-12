using Firebase.Auth;
using FirebaseAdmin.Auth;
using MediatR;

namespace WebAPI.Commands.AuthenticationCommands.RegisterCommands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserRecord>
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        public RegisterCommandHandler(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }
        public async Task<UserRecord> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userArg = new UserRecordArgs
            {
                Email = request.AuthTokenModel?.Email,
                Password = request.AuthTokenModel?.Password,
                DisplayName = request.AuthTokenModel?.DisplayName,
            };
            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArg);
            return userRecord;
        }
    }
}
