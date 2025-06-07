using MudBlazor;
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
            if (responseValue.Contains("EMAIL_EXISTS"))
                return "Email already exist.";
            return responseValue;
        }
    }
}

