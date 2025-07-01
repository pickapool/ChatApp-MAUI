using Blazored.LocalStorage;
using ChatApp_MAUI.AuthenticationProvider;
using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Shared.Services.CallBackServices.ChatRoomCallback;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Services.CallBackServices.LoadFriendsCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class ProfileContainerBase : ComponentBase
    {
        [Inject] protected ILocalStorageService _localStorage { get; set; } = default!;
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected ICallBackService _callBackService { get; set; } = default!;
        [Inject] protected IChatRoomCallbackService _chatRoomCallbackService { get; set; } = default!;
        [Inject] protected NavigationManager _navigationManager { get; set; } = default!;
        [Inject] protected AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        

        protected bool _open;
        protected void ToggleOpen() => _open = !_open;
        protected bool isLoading = true;
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            GlobalClass.Token = await _localStorage.GetItemAsStringAsync("token")?? string.Empty;
            if (string.IsNullOrEmpty(GlobalClass.Token))
            {
                _navigationManager.NavigateTo("/", true);
                return;
            }
            GlobalClass.User = await _loginService.GetUserRecord(GlobalClass.Token);
            GlobalClass.User ??= new();
            _notifierService.OnChanged += HandleChange;
            isLoading = false;
            await _callBackService.OnShowFrieds();
            await _chatRoomCallbackService.OnShowChatRoom();
        }
        private void HandleChange()
        {
            InvokeAsync(StateHasChanged);
        }
        public void Dispose()
        {
            _notifierService.OnChanged -= HandleChange;
        }
        protected async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationState)_authenticationStateProvider).NotifyUserLogout();
            _navigationManager.NavigateTo("/");
        }
        protected void GoToProfile()
        {
            _navigationManager.NavigateTo("/profile");
            _open = false;
        }
    }
}
