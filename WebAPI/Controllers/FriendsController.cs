using ChatApp_MAUI.Shared.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/friends/")]
    public class FriendsController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly IConfiguration _configuration;
        public FriendsController(IConfiguration config)
        {
            _configuration = config;
            _firestoreDb = FirestoreDb.Create(_configuration["FireStoreDbProjectId"]);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("getfriends")]
        public async Task<IActionResult> GetFriendsAsync([FromQuery] string uid)
        {
            try
            {
                var query = _firestoreDb.Collection("Friends")
                          .WhereEqualTo("From", uid)
                          .WhereEqualTo("IsAccepted", true);
                var snapshot = await query.GetSnapshotAsync();
                var friendsList = snapshot.Documents
                    .Select(doc => doc.ConvertTo<FriendsModel>())
                    .ToList();
                return Ok(friendsList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("addfriend")]
        public async Task<IActionResult> AddFriendAsync([FromBody] FriendsModel friendsModel)
        {
            try
            {
                var docRef = _firestoreDb.Collection("Friends").Document();
                friendsModel.Id = docRef.Id;
                await docRef.SetAsync(friendsModel);
                return Ok(new { Message = "Friend request sent successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("deletefriend")]
        public async Task<IActionResult> DeleteFriendAsync([FromBody] FriendsModel friendsModel)
        {
            try
            {
                var docRef = _firestoreDb.Collection("Friends").Document(friendsModel.Id);
                await docRef.DeleteAsync();
                return Ok(new { Message = "Friend request sent successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
