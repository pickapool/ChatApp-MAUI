using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Shared.Services.UserServices;
using ChatApp_MAUI.Shared.Services.FriendServices;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using Services.CallBackServices.LoadFriendsCallback;
using ChatApp_MAUI.Shared.Services.MessageServices;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class HomePageBase : ComponentBase, ICallBackService
    {
        [Inject] protected IFriendService _friendService { get; set; } = default!;
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected ICallBackService _callBackService { get; set; } = default!;
        [Inject] protected IMessageService _messageService { get; set; } = default!;
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        protected List<FriendsModel> friends = new();
        protected int loaderCount = 20;
        protected override async Task OnInitializedAsync()
        {
            
            _callBackService.RegisterCallback(this);
            _notifierService.OnChanged += HandleChange;
            if(_appStateService.User.Uid != null)
            {
                friends = await _friendService.GetFriends(_appStateService.User.Uid, _appStateService.Token);
            }
            StateHasChanged();
        }
        private void HandleChange()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _notifierService.OnChanged -= HandleChange;
        }
        public async Task OnShowFrieds()
        {
            friends = await _friendService.GetFriends(_appStateService.User.Uid, _appStateService.Token);
            StateHasChanged();
        }

        public void RegisterCallback(ICallBackService listener)
        {
            _callBackService = listener;
        }
    }
}
