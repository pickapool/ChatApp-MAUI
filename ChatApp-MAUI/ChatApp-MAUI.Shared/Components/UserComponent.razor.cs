using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.UserServices;
using ChatApp_MAUI.Shared.Common;
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
                SnackBarHelper.ShowSnackbar(reponse, Variant.Filled, _snackBar, Severity.Success);
            }
            catch (Exception ex)
            {
                SnackBarHelper.ShowSnackbar(ex.Message, Variant.Filled, _snackBar, Severity.Error);
            }
        }
    }
}
