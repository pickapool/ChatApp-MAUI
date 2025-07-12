using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Domain.Entities
{
    [FirestoreData]
    public class FriendsModel
    {
        [FirestoreProperty]
        public string? Id { get; set; }
        [FirestoreProperty]
        public string? From { get; set; }
        [FirestoreProperty]
        public string? To { get; set; }
        [FirestoreProperty]
        public bool IsAccepted { get; set; }
    }
}
