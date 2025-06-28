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
        public static SKBitmap CropCenterSquare(SKBitmap bitmap)
        {
            int size = Math.Min(bitmap.Width, bitmap.Height);
            int x = (bitmap.Width - size) / 2;
            int y = (bitmap.Height - size) / 2;

            // Create a new SKBitmap to hold the cropped subset  
            SKBitmap croppedBitmap = new SKBitmap(size, size);

            // Extract the subset using the correct method signature  
            bitmap.ExtractSubset(croppedBitmap, new SKRectI(x, y, x + size, y + size));

            return croppedBitmap;
        }
    }
}

