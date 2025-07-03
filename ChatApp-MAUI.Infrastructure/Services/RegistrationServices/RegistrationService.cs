using FirebaseAdmin.Auth;
using System.Net.Http.Json;

namespace ChatApp_MAUI.Infrastructure.Services.RegistrationServices
{
    public class RegistrationService : IRegistrationService
    {
        HttpClient _httpClient;
        public RegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task RegisterAsync(UserRecordArgs args)
        {
            if (string.IsNullOrEmpty(args.Email) || string.IsNullOrEmpty(args.Password) || string.IsNullOrEmpty(args.DisplayName))
            {
                throw new ArgumentException("All fields are required.");
            }
            try
            {
                var requestBody = new { AuthTokenModel = args };
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", requestBody);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{HttpErrorHelper.GetHttpError(error)}");
                }
                response.EnsureSuccessStatusCode();
            }
            catch (FirebaseAuthException ex)
            {
                throw new Exception($"Firebase error: {ex.Message}");
            }
        }
    }
}
