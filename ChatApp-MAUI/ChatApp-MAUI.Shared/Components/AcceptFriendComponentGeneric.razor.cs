using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.FriendServices;
using ChatApp_MAUI.Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class AcceptFriendGenericComponentBase : ComponentBase
    {
        [Inject] protected IUserService _userService { get; set; } = default!;
        [Inject] protected IFriendService _friendService { get; set; } = default!;
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        [Parameter] public FriendsModel? FriendRequestModel { get; set; }
        [Parameter] public bool ShowDeleteButton { get; set; }
        protected AuthTokenModel? User { get; set; }
        protected override async Task OnInitializedAsync()
        {
            FilterParameterModel param = new();
            param.IsUid = true;
            param.Uid = FriendRequestModel.From;
            param.Token = _appStateService.Token;
            User = await _userService.GetUserAccount(param);
            StateHasChanged();
        }
        protected void CloseNotification()
        {
            _snackBar.Clear();
        }

        protected async Task AcceptFriendRequest()
        {
            await _friendService.AccepFriendRequest(FriendRequestModel, _appStateService.Token);
            _snackBar.Clear();
        }
        protected async Task DeleteFriendRequest()
        {
            //await _friendService.Dele(FriendRequestModel, _appStateService.Token);
        }
    }
}
