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
        public MessagesController(IConfiguration config, FirestoreDb firestore) {
            _configuration = config;
            _firestoreDb = firestore;
        }
        [HttpPost]
        [Route("getchatroom")]
        public async Task<IActionResult> GetChatRoomAsync([FromBody] FilterParameterModel param)
        {
            try
            {
                var query = _firestoreDb.Collection("ChatRooms")
                    .WhereArrayContainsAny("Members", new[] { param.Uid, param.SenderUid });

                var snapshot = await query.GetSnapshotAsync();
                var chatRooms = snapshot.Documents
                    .Select(doc => doc.ConvertTo<ChatRoomModel>())
                    .ToList();
                return Ok(chatRooms);
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
    }
}
