using ChatApp_MAUI.Shared.Models;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/message/")]
    public class MessagesController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly IConfiguration _configuration;
        public MessagesController(IConfiguration config, FirestoreDb firestore)
        {
            _configuration = config;
            _firestoreDb = firestore;
        }
        [HttpPost]
        [Route("getchatroom")]
        public async Task<IActionResult> GetChatRoomAsync([FromBody] FilterParameterModel param)
        {
            try
            {
                var document = await _firestoreDb.Collection("ChatRooms").GetSnapshotAsync();

                if (document.Any())
                {


                    var query = _firestoreDb.Collection("ChatRooms")
                    .WhereArrayContainsAny("Members", new[] { param.Uid, param.SenderUid });

                    var snapshot = await query.GetSnapshotAsync();
                    var chatRooms = snapshot.Documents
                        .Select(doc => doc.ConvertTo<ChatRoomModel>())
                        .ToList();
                    return Ok(chatRooms);
                }
                return Ok(new List<ChatRoomModel>());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("getconversation")]
        public async Task<IActionResult> GetConversation([FromBody] FilterParameterModel param)
        {
            try
            {
                var document = await _firestoreDb.Collection("ChatRooms").GetSnapshotAsync();

                if (document.Any())
                {
                    var query = _firestoreDb.Collection("ChatRooms")
                    .WhereArrayContainsAny("Members", new[] { param.Uid, param.SenderUid });

                    var snapshot = await query.GetSnapshotAsync();
                    var chatRooms = snapshot.Documents
                        .Select(doc => doc.ConvertTo<ChatRoomModel>())
                        .ToList();
                    var messages = chatRooms.FirstOrDefault()?.Messages ?? new();
                    return Ok(messages);
                }
                return Ok(new List<MessageModel>());

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("addmessage")]
        public async Task<IActionResult> AddMessage([FromBody] MessageModel param)
        {
            var query = _firestoreDb.Collection("ChatRooms")
                .WhereArrayContainsAny("Members", new[] { param.From, param.To });

            var snapshot = await query.GetSnapshotAsync();

            // Find a chat room where BOTH UIDs exist
            var chatRoomDoc = snapshot.Documents
                .FirstOrDefault(doc =>
                {
                    var members = doc.GetValue<List<string>>("Members");
                    return members != null && members.Contains(param.To) && members.Contains(param.From);
                });

            if (chatRoomDoc == null)
            {
                // Generate custom ID
                var chatRoomId = Guid.NewGuid().ToString();

                var chatRoom = new ChatRoomModel
                {
                    Id = chatRoomId,
                    members = new List<string> { param.From, param.To }
                };

                // Set document using your custom ID
                var docRef = _firestoreDb.Collection("ChatRooms").Document(chatRoomId);
                await docRef.SetAsync(chatRoom);

                // Create message and set message Id before saving
                var messageId = Guid.NewGuid().ToString();
                var message = new MessageModel
                {
                    Id = messageId,
                    To = param.To,
                    From = param.From,
                    Text = param.Text,
                    CreatedAt = DateTime.UtcNow
                };

                await docRef.Collection("Messages").Document(messageId).SetAsync(message);
            }
            else
            {
                // ChatRoom exists — save message in its Messages subcollection
                var messageId = Guid.NewGuid().ToString();
                var message = new MessageModel
                {
                    Id = messageId,
                    To = param.To,
                    From = param.From,
                    Text = param.Text,
                    CreatedAt = DateTime.UtcNow
                };

                await chatRoomDoc.Reference.Collection("Messages").Document(messageId).SetAsync(message);
            }


            return Ok();
        }
    }
}
