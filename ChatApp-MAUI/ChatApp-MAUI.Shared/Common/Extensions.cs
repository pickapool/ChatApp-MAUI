using MudBlazor;
using Newtonsoft.Json;
namespace ChatApp_MAUI.Shared.Common
{
    public static class Extensions
    {
        public static void ShowSnackbar(string message, Variant variant, ISnackbar snackbar, Severity severity)
        {
            snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
            snackbar.Configuration.SnackbarVariant = variant;
            snackbar.Configuration.MaxDisplayedSnackbars = 10;
            snackbar.Configuration.VisibleStateDuration = 2000;
            snackbar.Add($"{message}", severity);
        }
        public static string GetHttpError(string responseValue)
        {
            if (responseValue.ToLower().Contains("email_exist"))
                return "Email already exist.";
            if(responseValue.ToLower().Contains("invalid email"))
                return "Invalid email address.";
            return responseValue;
        }
        public static T Clone<T>(this T source)
        {
            if (ReferenceEquals(source, null)) return default;
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}

