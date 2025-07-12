using ChatApp_MAUI.Shared.Components;
using ChatApp_MAUI.Domain.Entities;
using MudBlazor;
namespace ChatApp_MAUI.Shared.Common
{
    public static class SnackBarHelper
    {
        public static void ShowFriendRequestNotification(ISnackbar snackbarService, FriendsModel model)
        {
            snackbarService.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
            snackbarService.Configuration.SnackbarVariant = Variant.Filled;
            snackbarService.Configuration.VisibleStateDuration = 5000;
            var config = (SnackbarOptions options) =>
            {
                options.DuplicatesBehavior = SnackbarDuplicatesBehavior.Prevent;
                options.RequireInteraction = true;
                options.HideIcon = true;
                options.ShowCloseIcon = false;
            };

            var parameters = new Dictionary<string, object>
            {
                { "FriendRequestModel", model }
            };

            snackbarService.Add<AcceptFriendComponent<FriendsModel>>(parameters, configure: config);

        }
        public static void ShowSnackbar(string message, Variant variant, ISnackbar snackbar, Severity severity)
        {
            snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
            snackbar.Configuration.SnackbarVariant = variant;
            snackbar.Configuration.MaxDisplayedSnackbars = 10;
            snackbar.Configuration.VisibleStateDuration = 2000;
            snackbar.Add($"{message}", severity);
        }
    }
}
