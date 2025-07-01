using ChatApp_MAUI.Shared.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.SignalRHub;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/friends/")]
    public class FriendsController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FriendsController(IConfiguration config, FirestoreDb firestore, IHubContext<NotificationHub> hubContext)
        {
            _configuration = config;
            _firestoreDb = firestore;
            _hubContext = hubContext;
        }
        [HttpPost]
        [Route("getfriends")]
        public async Task<IActionResult> GetFriendsAsync([FromBody] string uid)
        {
            try
            {
                var fromFilter = Filter.EqualTo("From", uid);
                var toFilter = Filter.EqualTo("To", uid);

                var query = _firestoreDb.Collection("Friends")
                          .Where(Filter.Or(fromFilter, toFilter))
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
                var query = _firestoreDb.Collection("Friends")
                            .WhereEqualTo("From", friendsModel.From)
                            .WhereEqualTo("To", friendsModel.To)
                            .Limit(1);

                var snapshot = await query.GetSnapshotAsync();
                if (snapshot.Count > 0)
                {
                    var existing = snapshot.First().ConvertTo<FriendsModel>();
                    if (existing.IsAccepted)
                        return Conflict("You are already friends.");

                    return Conflict("Friend request already sent.");
                }

                var reverseQuery = _firestoreDb.Collection("Friends")
                    .WhereEqualTo("From", friendsModel.To)
                    .WhereEqualTo("To", friendsModel.From)
                    .Limit(1);

                var reverseSnapshot = await reverseQuery.GetSnapshotAsync();
                if (reverseSnapshot.Count > 0)
                {
                    var existing = reverseSnapshot.First().ConvertTo<FriendsModel>();
                    if (existing.IsAccepted)
                        return Conflict("You are already friends.");

                    return Conflict("User has already sent you a request.");
                }

                var docRef = _firestoreDb.Collection("Friends").Document();
                friendsModel.Id = docRef.Id;
                await docRef.SetAsync(friendsModel);

                await NotificationExtensions.NotifyAsync(_hubContext, friendsModel);

                return Ok("Friend request sent successfully.");
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
        [HttpPost]
        [Route("acceptfriend")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] FriendsModel friendsModel)
        {
            try
            {
                var docRef = _firestoreDb.Collection("Friends").Document(friendsModel.Id);
                await docRef.UpdateAsync(
                    new Dictionary<string, object>
                    {
                        { "IsAccepted", true }
                    });
                return Ok(new { Message = "Friend request sent accepted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
