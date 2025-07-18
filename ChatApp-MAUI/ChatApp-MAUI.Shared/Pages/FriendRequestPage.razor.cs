using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.FriendRequestCallback;
using ChatApp_MAUI.Infrastructure.Services.FriendServices;
using Microsoft.AspNetCore.Components;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class FriendRequestPageBase : ComponentBase, IFriendRequestCallback
    {
        [Inject] protected IFriendService _friendService { get; set; } = default!;
        [Inject] protected IFriendRequestCallback _friendRequestCallBack { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        
        protected List<FriendsModel> friendRequests = new();
        protected override async Task OnInitializedAsync()
        {
            if (_appStateService.User?.Uid != null)
            {
                var result = await _friendService.GetFriendRequest(_appStateService.User.Uid, _appStateService.Token);
                friendRequests = result.ToList();
            }
            _friendRequestCallBack.RegisterCallback(this);
        }

        public void RegisterCallback(IFriendRequestCallback listener)
        {
            _friendRequestCallBack = listener;
        }

        public async Task OnShowRequest(AppStateService app)
        {
            var result = await _friendService.GetFriendRequest(app.User.Uid, app.Token);
            friendRequests = result.ToList();
            StateHasChanged();
        }
    }
}
