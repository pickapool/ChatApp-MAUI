using ChatApp_MAUI.Infrastructure.Services.RegistrationServices;
using ChatApp_MAUI.Shared.Common;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Extensions = ChatApp_MAUI.Shared.Common.Extensions;
namespace ChatApp_MAUI.Shared.Pages
{
    public partial class AccountRegistrationBase : ComponentBase
    {
        //Services
        [Inject] protected ISnackbar _snackBar { get; set; } = default!;
        [Inject] protected IRegistrationService _registrationService { get; set; } = default!;
        //Parameters
        [Parameter] public EventCallback OnSignIn { get; set; }
        //Instance
        protected UserRecordArgs userRecord = new();
        protected string confirmPassword = string.Empty;
        protected bool _open = false;
        protected bool isLoading, isShow;
        protected InputType PasswordInput = InputType.Password;
        protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        protected async Task Register()
        {
            isLoading = true;
            await Task.Delay(100);
            try
            {
                var passwordValidation = ValidatePassword(userRecord.Password);

                if (String.IsNullOrEmpty(userRecord.Email) || String.IsNullOrEmpty(userRecord.Password) || String.IsNullOrEmpty(confirmPassword))
                {
                    SnackBarHelper.ShowSnackbar("All fields are required.", Variant.Filled, _snackBar, Severity.Error);
                    return;
                }
                if (userRecord.Password != confirmPassword)
                {
                    SnackBarHelper.ShowSnackbar("Passwords do not match.", Variant.Filled, _snackBar, Severity.Error);
                    return;
                }
                if (passwordValidation != null)
                {
                    SnackBarHelper.ShowSnackbar(passwordValidation, Variant.Filled, _snackBar, Severity.Error);
                    return;
                }
                await _registrationService.RegisterAsync(userRecord);
                SnackBarHelper.ShowSnackbar("Account created successfully, please login to continue.", Variant.Filled, _snackBar, Severity.Success);
                await GoToSignIn();
            } catch (Exception ee) {

                SnackBarHelper.ShowSnackbar(ee.Message, Variant.Filled, _snackBar, Severity.Error);
            } finally {
                isLoading = false;
            }
        }
        protected async Task GoToSignIn()
        {
            await OnSignIn.InvokeAsync();
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
        protected string ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return "Password is required.";
            if (password.Length < 8)
                return "Password must be at least 8 characters long.";
            if (!password.Any(char.IsUpper))
                return "Password must contain at least one uppercase letter.";
            if (!password.Any(char.IsDigit))
                return "Password must contain at least one number.";
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return "Password must contain at least one special character.";
            return null; // Valid
        }
    }
}
