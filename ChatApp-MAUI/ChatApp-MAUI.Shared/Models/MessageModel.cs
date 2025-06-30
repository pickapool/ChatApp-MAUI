using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Models
{
    [FirestoreData]
    public class MessageModel
    {
        [FirestoreProperty]
        public string? From { get; set; }
        [FirestoreProperty]
        public string? To { get; set; }
        [FirestoreProperty]
        public string? Text { get; set; }
        [FirestoreProperty]
        public MessageModel? Messages { get; set; }
    }
}
