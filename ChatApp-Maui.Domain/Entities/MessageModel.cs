using ChatApp_MAUI.Domain.Common;
using Google.Cloud.Firestore;

namespace ChatApp_MAUI.Domain.Entities
{
    [FirestoreData]
    public class MessageModel
    {
        [FirestoreProperty]
        public string? Id { get; set; }
        [FirestoreProperty]
        public string? From { get; set; }
        [FirestoreProperty]
        public string? To { get; set; }
        [FirestoreProperty]
        public string? Text { get; set; }
        [FirestoreProperty]
        public Enums.MessageType Type { get; set; }
        [FirestoreProperty]
        public DateTime? CreatedAt { get; set; }
        [FirestoreProperty]
        public string? ChatRoomId { get; set; }
    }
}
