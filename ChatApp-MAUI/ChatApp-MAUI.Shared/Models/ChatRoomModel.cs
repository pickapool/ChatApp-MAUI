using ChatApp_MAUI.Shared.Common;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Models
{
    [FirestoreData]
    public class ChatRoomModel
    {
        public string? uid { get; set; }
        [FirestoreProperty]
        public List<string>? members { get; set; }
        [FirestoreProperty]
        public string? Text { get; set; }
        [FirestoreProperty]
        public Enums.MessageType Type { get; set; }
    }
}
