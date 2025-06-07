using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth/")] 
    public class FirebaseAuthController : ControllerBase
    {
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
            catch (FirebaseAuthException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
