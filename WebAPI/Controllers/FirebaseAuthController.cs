using ChatApp_MAUI.Shared.Models;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth/")] 
    public class FirebaseAuthController : ControllerBase
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;
        public FirebaseAuthController(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]UserRecordArgs args)
        {
            try
            {   
                var userArg = new UserRecordArgs
                {
                    Email = args.Email,
                    Password = args.Password,
                    DisplayName = args.DisplayName,
                };
                var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArg);
                return Ok(userRecord);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            } 
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]UserRecordArgs args)
        {
            try
            {
                var response = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(args.Email, args.Password);
                var token = await response.User.GetIdTokenAsync();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("getuser")]
        public async Task<IActionResult> GetUserAsync([FromBody] string idToken)
        {
            try
            {
                idToken = idToken.Trim('"');
                var verifiedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                if(verifiedToken == null)
                {
                    return Unauthorized(new { Error = "UnAuthorized" });
                }
                UserRecord userRecord = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.GetUserAsync(verifiedToken.Uid);
                return Ok(userRecord);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("verifyemail")]
        public async Task<IActionResult> VerifyAccount([FromBody] string email)
        {
            try
            {
                var response = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(email);
                return Ok(response.ToString());
            } catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("updatephoto")]
        public async Task<IActionResult> UpdatePhoto([FromBody] AuthTokenModel record)
        {
            try
            {
                var response = await FirebaseAuth.DefaultInstance.UpdateUserAsync(new UserRecordArgs
                {
                    Uid = record.Uid,
                    PhotoUrl = record.PhotoUrl,
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("updateprofile")]
        public async Task<IActionResult> UpdateProfile([FromBody] AuthTokenModel record)
        {
            try
            {
                var response = await FirebaseAuth.DefaultInstance.UpdateUserAsync(new UserRecordArgs
                {
                    Uid = record.Uid,
                    DisplayName = record.DisplayName,
                    PhotoUrl = record.PhotoUrl,
                    PhoneNumber = record.PhoneNumber
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("searchuser")]
        public async Task<IActionResult> SearchUserAsync([FromBody] FilterParameterModel param)
        {
            try
            {
                var verifiedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(param.Token.Trim('"'));
                if (verifiedToken == null)
                {
                    return Unauthorized(new { Error = "UnAuthorized" });
                }

                var listOfUsers = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
                var UserRecords = new List<ExportedUserRecord>();
                await foreach (var user in listOfUsers)
                {
                    if (param.Uid == user.Uid)
                        continue;
                    if (param.IsName &&
                        !string.IsNullOrWhiteSpace(user.DisplayName) &&
                        user.DisplayName.ToLower().Contains(param.Name.ToLower()))
                    {
                        UserRecords.Add(user);
                    }
                    else if (!param.IsName) // if no filter, add all users
                    {
                        UserRecords.Add(user);
                    }
                }

                return Ok(UserRecords);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost]
        [Route("getuseraccount")]
        public async Task<IActionResult> GetCurrentUserASync([FromBody] FilterParameterModel param)
        {
            try
            {
                var verifiedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(param.Token.Trim('"'));
                if (verifiedToken == null)
                {
                    return Unauthorized(new { Error = "UnAuthorized" });
                }
                var listOfUsers = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
                var currentUser = await listOfUsers.Where(e => e.Uid == param.Uid).FirstOrDefaultAsync();
                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
