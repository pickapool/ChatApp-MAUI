using Google.Cloud.Firestore;

namespace ChatApp_MAUI.Domain.Entities
{
    [FirestoreData]
    public class ChatRoomModel
    {
        [FirestoreProperty]
        public string? Id { get; set; }
        [FirestoreProperty]
        public List<string>? members { get; set; }
        [FirestoreProperty("messages")]
        public List<MessageModel>? Messages { get; set; }
    }
}
