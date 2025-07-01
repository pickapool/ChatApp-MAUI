using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class UserComponentBase : ComponentBase
    {
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected IUserService _userService { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        [Parameter] public AuthTokenModel? User { get; set; }
        protected async Task SendFriendRequest(AuthTokenModel user)
        {
            FriendsModel friend = new();
            friend.From = _appStateService.User?.Uid;
            friend.To = user.Uid;
            friend.IsAccepted = false;
            try
            {
                var reponse = await _userService.SendFriendRequest(friend, _appStateService.Token);
                Extensions.ShowSnackbar(reponse, Variant.Filled, _snackBar, Severity.Success);
            }
            catch (Exception ex)
            {
                Extensions.ShowSnackbar(ex.Message, Variant.Filled, _snackBar, Severity.Error);
            }
        }
    }
}
