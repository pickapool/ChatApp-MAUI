using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.UserServices;
using ChatApp_MAUI.Shared.Services.FriendServices;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Shared.Services.CallBackServices;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class HomePageBase : ComponentBase, ICallBackService
    {
        [Inject] protected IFriendService _friendService { get; set; } = default!;
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected ICallBackService _callBackService { get; set; } = default!;
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        protected List<FriendsModel> friends = new();
        protected override void OnInitialized()
        {
            _callBackService.RegisterCallback(this);
            _notifierService.OnChanged += HandleChange;
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
            friends = await _friendService.GetFriends(GlobalClass.User.Uid, GlobalClass.Token);
            StateHasChanged();
        }

        public void RegisterCallback(ICallBackService listener)
        {
            _callBackService = listener;
        }
    }
}
