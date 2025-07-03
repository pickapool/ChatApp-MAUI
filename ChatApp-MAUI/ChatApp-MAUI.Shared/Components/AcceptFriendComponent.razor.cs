using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Shared.Services.FriendServices;
using ChatApp_MAUI.Shared.Services.UserServices;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class AcceptFriendComponent : ComponentBase
    {
        [Inject] protected IUserService _userService { get; set; } = default!;
        [Inject] protected IFriendService _friendService { get; set; } = default!;
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        [Parameter] public FriendsModel? FriendRequestModel { get; set; }
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
    }
}
