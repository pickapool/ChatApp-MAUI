using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Messaging;
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
                var chatRoomRef = _firestoreDb.Collection("ChatRooms").Document(param.ChatRoomId);
                var chatRoomSnapshot = await chatRoomRef.GetSnapshotAsync();

                if (!chatRoomSnapshot.Exists)
                {
                    return Ok();
                }

                var messagesSnapshot = await chatRoomRef
                    .Collection("Messages")
                    .OrderBy("CreatedAt")
                    .GetSnapshotAsync();

                var messages = messagesSnapshot.Documents
                    .Select(doc =>
                    {
                        var msg = doc.ConvertTo<MessageModel>();
                        msg.Id = doc.Id;
                        return msg;
                    })
                    .ToList();

                return Ok(messages);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("getchatroomid")]
        public async Task<IActionResult> GetChatRoomId([FromBody] FilterParameterModel param)
        {
            try
            {
                var query = _firestoreDb.Collection("ChatRooms");

                var snapshot = await query.GetSnapshotAsync();

                var chatRoomDoc = snapshot.Documents
                    .FirstOrDefault(doc =>
                    {
                        var members = doc.GetValue<List<string>>("members");
                        return members != null && members.Contains(param.SenderUid) && members.Contains(param.Uid);
                    });

                if (chatRoomDoc == null && snapshot.Count <= 0)
                {
                    var chatRoomId = Guid.NewGuid().ToString();

                    var chatRoom = new ChatRoomModel
                    {
                        Id = chatRoomId,
                        members = new List<string> { param.Uid, param.SenderUid }
                    };
                    var docRef = _firestoreDb.Collection("ChatRooms").Document(chatRoomId);
                    await docRef.SetAsync(chatRoom);
                    return Ok(chatRoomId);
                } else
                {
                    return Ok(chatRoomDoc.Id);
                }
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
            var docRef = _firestoreDb.Collection("ChatRooms").Document(param.ChatRoomId);
            var snapshot = await docRef.GetSnapshotAsync();

            var messageId = Guid.NewGuid().ToString();
            var message = new MessageModel
            {
                Id = messageId,
                To = param.To,
                From = param.From,
                Text = param.Text,
                CreatedAt = DateTime.UtcNow,
                ChatRoomId = param.ChatRoomId
            };

            await docRef.Collection("Messages").Document(messageId).SetAsync(message);


            return Ok();
        }
    }
}
