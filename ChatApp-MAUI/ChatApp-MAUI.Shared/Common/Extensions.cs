using MudBlazor;
using Newtonsoft.Json;
using SkiaSharp;
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
            if (responseValue.ToLower().Contains("invalid email"))
                return "Invalid email address.";
            return responseValue;
        }
        public static T Clone<T>(this T source)
        {
            if (ReferenceEquals(source, null)) return default;
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
        public static string GetTimeAgo(DateTime? dateTime)
        {
            if (dateTime == null)
                return "Unknown time";

            var timeSpan = DateTime.UtcNow - dateTime.Value.ToUniversalTime();

            if (timeSpan.TotalMinutes < 1)
                return "Just now";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minute{(timeSpan.TotalMinutes >= 2 ? "s" : "")} ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hour{(timeSpan.TotalHours >= 2 ? "s" : "")} ago";
            if (timeSpan.TotalDays < 30)
                return $"{(int)timeSpan.TotalDays} day{(timeSpan.TotalDays >= 2 ? "s" : "")} ago";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} month{(timeSpan.TotalDays >= 60 ? "s" : "")} ago";

            return $"{(int)(timeSpan.TotalDays / 365)} year{(timeSpan.TotalDays >= 730 ? "s" : "")} ago";
        }

    }
}

