using Blazored.LocalStorage;
using ChatApp_MAUI.AuthenticationProvider;
using ChatApp_MAUI.Shared.Services;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;
namespace ChatApp_MAUI.Shared.Components
{
    public partial class LoginComponentBase : ComponentBase
    {
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected ILocalStorageService _localStorage { get; set; } = default!;
        [Inject] protected IFormFactor _formFactor { get; set; } = default!;
        [Parameter] public EventCallback OnRegistrationClick { get; set; }

        protected string email = string.Empty, password = string.Empty;
        protected bool _open = false, isLoading = false, isShow = false;
        protected InputType PasswordInput = InputType.Password;
        protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        protected string factor => _formFactor.GetFormFactor();
        protected string platform => _formFactor.GetPlatform();

        protected async void RegisterClick()
        {
            await OnRegistrationClick.InvokeAsync();
        }
        protected void ToggleDrawer()
        {
            _open = !_open;
        }
        protected async Task HandleKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await Login();
            }
        }
        protected async Task Login()
        {
            isLoading = true;
            await Task.Delay(500);
            StateHasChanged();
            var account = new UserRecordArgs()
            {
                Email = email,
                Password = password,
            };
            try
            {
                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
                {
                    Extensions.ShowSnackbar("All fields are required", Variant.Filled, _snackBar, Severity.Error);
                    return;
                }
                var uid = await _loginService.Authenticate(account);
                await _localStorage.SetItemAsync("token", uid);
                ((CustomAuthenticationState)_authenticationStateProvider).NotifyUserAuthentication(uid);
                email = string.Empty;
                password = string.Empty;
            } catch (Exception ex) {
                if (ex.Message.ToLower().Contains("invalid"))
                    Extensions.ShowSnackbar("Invalid email or password", Variant.Filled, _snackBar, Severity.Error);
                else
                    Extensions.ShowSnackbar(ex.Message, Variant.Filled, _snackBar, Severity.Error);
            } finally {
                isLoading = false;
            }
        }
        protected void ShowPassword()
        {
            if(isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            } else {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}
