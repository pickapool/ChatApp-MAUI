using ChatApp_MAUI.AuthenticationProvider;
using ChatApp_MAUI.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class ProfileContainer : ComponentBase
    {
        protected AuthTokenModel user;
        protected bool _open;
        protected string DefaultPhoto = "/images/blank-profile.webp";
        protected void ToggleOpen() => _open = !_open;
        protected bool isLoading = true;
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            string token = await _localStorage.GetItemAsStringAsync("token");
            user = await _loginService.GetUserRecord(token);
            isLoading = false;
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
