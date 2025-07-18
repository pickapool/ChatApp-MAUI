using ChatApp_MAUI.Domain.Entities;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using MediatR;

namespace WebAPI.Commands.FriendRequestCommands.GetCommand
{
    public class GetFriendRequestCommandHandler : IRequestHandler<GetFriendRequestCommand, IEnumerable<FriendsModel>>
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        private readonly FirestoreDb _firestoreDb;
        public GetFriendRequestCommandHandler(FirebaseAuthClient firebaseAuthClient, FirestoreDb firestoreDb)
        {
            _firebaseAuthClient = firebaseAuthClient;
            _firestoreDb = firestoreDb;
        }

        public async Task<IEnumerable<FriendsModel>> Handle(GetFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var docRef = _firestoreDb.Collection("Friends")
                .WhereEqualTo("To", request.uid)
                .WhereEqualTo("IsAccepted", false);
            var snapshot = await docRef.GetSnapshotAsync();

            var results = snapshot.Documents
                .Where(doc => doc.Exists)
                .Select(doc => doc.ConvertTo<FriendsModel>());
            return results;
        }
    }
}
