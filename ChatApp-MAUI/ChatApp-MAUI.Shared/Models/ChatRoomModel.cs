using Google.Cloud.Firestore;

namespace ChatApp_MAUI.Shared.Models
{
    [FirestoreData]
    public class ChatRoomModel
    {
        [FirestoreProperty]
        public string? Id { get; set; }
        [FirestoreProperty]
        public List<string>? members { get; set; }
        [FirestoreProperty]
        public List<MessageModel>? Messages { get; set; }
    }
}
