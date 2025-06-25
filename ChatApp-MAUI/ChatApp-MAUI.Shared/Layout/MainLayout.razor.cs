using Microsoft.AspNetCore.Components;

namespace ChatApp_MAUI.Shared.Layout
{
    public partial class MainLayoutBase : LayoutComponentBase
    {
        protected ElementReference reference;
        protected string email = string.Empty, password = string.Empty;
        protected bool _open = false, isLoading = false, isShow = false, isRegistration = false;
        protected bool _isToggling = false;

        protected async Task ToggleDrawer()
        {
            if (_isToggling) return;

            _isToggling = true;
            _open = !_open;

            await Task.Delay(300); // debounce delay
            _isToggling = false;
        }
    }
}
