using ChatApp_MAUI.Domain.Entities;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using MediatR;

namespace WebAPI.Commands.FriendRequestCommands.DeletCommand
{
    public class DeleteFriendRequestCommandHandler : IRequestHandler<DeleteFriendRequestCommand, int>
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        private readonly FirestoreDb _firestoreDb;
        public DeleteFriendRequestCommandHandler(FirebaseAuthClient firebaseAuthClient, FirestoreDb firestoreDb)
        {
            _firebaseAuthClient = firebaseAuthClient;
            _firestoreDb = firestoreDb;
        }

        public async Task<int> Handle(DeleteFriendRequestCommand request, CancellationToken cancellationToken)
        {
            await _firestoreDb.Collection("Friends").Document(request.documentID).DeleteAsync(null, cancellationToken);
            return StatusCodes.Status200OK;
        }
    }
}
