﻿using Blazored.LocalStorage;
using ChatApp_MAUI.AuthenticationProvider;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.CallBackServices.LoadFriendsCallback;
using ChatApp_MAUI.Infrastructure.Services;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ChatRoomCallback;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.FriendRequestCallback;
using ChatApp_MAUI.Infrastructure.Services.CustomAuthenticationServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class ProfileContainerBase : ComponentBase
    {
        [Inject] protected ILocalStorageService _localStorage { get; set; } = default!;
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected ICallBackService _callBackService { get; set; } = default!;
        [Inject] protected IChatRoomCallbackService _chatRoomCallbackService { get; set; } = default!;
        [Inject] protected IFriendRequestCallback _friendRequestCallBack { get; set; } = default!;
        [Inject] protected NavigationManager _navigationManager { get; set; } = default!;
        [Inject] protected AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;

        protected bool _open;
        protected void ToggleOpen() => _open = !_open;
        protected bool isLoading = true;
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            _appStateService.Token = await _localStorage.GetItemAsStringAsync("token")?? string.Empty;
            if (string.IsNullOrEmpty(_appStateService.Token))
            {
                _navigationManager.NavigateTo("/", true);
                return;
            }
            _appStateService.User = await _loginService.GetUserRecord(_appStateService.Token);
            _appStateService.User ??= new();
            _notifierService.OnChanged += HandleChange;
            isLoading = false;
            await _friendRequestCallBack.OnShowRequest(_appStateService);
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
