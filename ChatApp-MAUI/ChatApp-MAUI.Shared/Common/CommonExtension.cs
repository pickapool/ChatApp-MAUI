using MudBlazor;
public static class CommonExtension
{
    public static void ShowSnackbar(string message, Variant variant, ISnackbar snackbar, Severity severity)
    {
        snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
        snackbar.Configuration.SnackbarVariant = variant;
        snackbar.Configuration.MaxDisplayedSnackbars = 10;
        snackbar.Configuration.VisibleStateDuration = 2000;
        snackbar.Add($"{message}", severity);
    }
}

