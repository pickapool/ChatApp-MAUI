using ChatApp_MAUI.Shared.Models;
using FirebaseAdmin.Messaging;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.SignalRHub;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/message/")]
    public class MessagesController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly IConfiguration _configuration;
        readonly IHubContext<NotificationHub> _hubContext;
        public MessagesController(IConfiguration config, FirestoreDb firestore, IHubContext<NotificationHub> hubContext)
        {
            _configuration = config;
            _firestoreDb = firestore;
            _hubContext = hubContext;
        }
        [HttpPost]
        [Route("getchatroom")]
        public async Task<IActionResult> GetChatRoomAsync([FromBody] FilterParameterModel param)
        {
            try
            {
                var query = _firestoreDb.Collection("ChatRooms")
                        .WhereArrayContains("members", param.Uid);
                var snapshot = await query.GetSnapshotAsync();
                List<ChatRoomModel> chats = new();
                foreach (var doc in snapshot.Documents)
                {
                    ChatRoomModel chatRoomModel = doc.ConvertTo<ChatRoomModel>();
                    var chatRoomRef = _firestoreDb.Collection("ChatRooms").Document(doc.Id);
                    var chatRoomSnapshot = await chatRoomRef.GetSnapshotAsync();
                    var messagesSnapshot = await chatRoomRef
                        .Collection("Messages")
                        .OrderByDescending("CreatedAt")
                        .Limit(1)
                        .GetSnapshotAsync();

                    var messages = messagesSnapshot.Documents
                        .Select(doc =>
                        {
                            var msg = doc.ConvertTo<MessageModel>();
                            msg.Id = doc.Id;
                            return msg;
                        }).FirstOrDefault();
                    chatRoomModel.Messages ??= new();
                    messages ??= new();
                    chatRoomModel.Messages.Add(messages);
                    chats.Add(chatRoomModel);
                }
                return Ok(chats);
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

            await NotificationExtensions.NotifyMessageAsync(_hubContext, message);
            return Ok();
        }
    }
}
