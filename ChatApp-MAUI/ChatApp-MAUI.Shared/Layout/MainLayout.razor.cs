using ChatApp_MAUI.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChatApp_MAUI.Shared.Layout
{
    public partial class MainLayoutBase : LayoutComponentBase
    {
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        protected ElementReference reference;
        protected string email = string.Empty, password = string.Empty;
        protected bool _open = false, isLoading = false, isShow = false, isRegistration = false;
        protected bool _isToggling = false;
        protected MudTheme _theme = new();
        protected bool _isDarkMode;
        protected override void OnInitialized()
        {
            _notifierService.OnChanged += HandleChange;
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
