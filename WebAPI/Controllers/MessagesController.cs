using ChatApp_MAUI.Shared.Models;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/message/")]
    public class MessagesController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly IConfiguration _configuration;
        public MessagesController(IConfiguration config) {
            _configuration = config;
            _firestoreDb = FirestoreDb.Create(_configuration["FireStoreDbProjectId"]);
        }
        [Authorize]
        [HttpHead]
        [Route("getchatroom")]
        public async Task<IActionResult> GetMessageAsync([FromQuery] string uid)
        {
            try
            {
                var query = _firestoreDb.Collection("ChatRooms")
                         .WhereArrayContains("Members", uid);
                         //for indivual message .OrderByDescending("Messages.TimeStamp");

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
    }
}
