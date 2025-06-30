using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Shared.Services.INotificationServices;
using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Shared.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using MudBlazor;

namespace ChatApp_MAUI.Shared.Layout
{
    public partial class MainLayoutBase : LayoutComponentBase
    {
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        [Inject] protected IUserService _userService { get; set; } = default!;
        [Inject] protected INotificationService _notificationService { get; set; } = default!;
        protected ElementReference reference;
        protected string email = string.Empty, password = string.Empty;
        protected bool _open = false, isLoading = false, isShow = false, isRegistration = false;
        protected bool _isToggling = false;
        protected MudTheme _theme = new();
        protected bool _isDarkMode;
        protected AuthTokenModel? user;
        protected override void OnInitialized()
        {
            _notificationService.CreateNotificationBuilder().StartAsync();
            _notificationService.CreateNotificationBuilder().On<FriendsModel>("NotifyFriendRequest", model => {
                Console.WriteLine("Hello");
            });
            _notifierService.OnChanged += HandleChange;
        }
        protected async Task<IEnumerable<AuthTokenModel>> GetUsers(string name, CancellationToken t)
        {
            //Show progreee indicator
            await Task.Delay(1000);
            FilterParameterModel param = new();
            param.IsName = true;
            param.Name = name;
            param.Token = GlobalClass.Token;

            return await _userService.SearchUsers(param);
        }
        protected async void ToggleDrawer()
        {
            if (_isToggling) return;

            _isToggling = true;
            _open = !_open;

            await Task.Delay(100); // debounce delay
            _isToggling = false;
        }
        private void HandleChange()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _notifierService.OnChanged -= HandleChange;
        }
    }
}
