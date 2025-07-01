using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Shared.Services.INotificationServices;
using ChatApp_MAUI.Shared.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using MudBlazor;
using Newtonsoft.Json;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;

namespace ChatApp_MAUI.Shared.Layout
{
    public partial class MainLayoutBase : LayoutComponentBase
    {
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        [Inject] protected IUserService _userService { get; set; } = default!;
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected IConfiguration _configuration { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;

        protected ElementReference reference;
        protected string email = string.Empty, password = string.Empty;
        protected bool _open = false, isLoading = false, isShow = false, isRegistration = false;
        protected bool _isToggling = false;
        protected MudTheme _theme = new();
        protected AuthTokenModel? user;
        protected override async Task OnInitializedAsync()
        {
            var hubConnection = NotificationService.GetConnection($"{_configuration["BaseAPI:Url"]}/NotificationHub");
            hubConnection.On<FriendsModel>("NotifyFriendRequest", model =>
            {
                if (model.To == _appStateService.User.Uid)
                {
                    SnackBarHelper.ShowFriendRequestNotification(_snackBar, model);
                }
            });

            await hubConnection.StartAsync();
            _notifierService.OnChanged += HandleChange;
        }
        protected void ToggleDarkMode()
        {
            _appStateService.IsDarkMode = !_appStateService.IsDarkMode;
            _notifierService.NotifyChanged();
        }
        protected async Task<IEnumerable<AuthTokenModel>> GetUsers(string name, CancellationToken t)
        {
            //Show progreee indicator
            await Task.Delay(500);
            FilterParameterModel param = new();
            param.IsName = true;
            param.Name = name;
            param.Token = _appStateService.Token;
            param.Uid = _appStateService.User.Uid;

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
