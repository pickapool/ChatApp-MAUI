using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class PreLoaderComponentBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildComponent { get; set; } = default!;
        [Inject] protected AuthenticationStateProvider _authenticationStateProvider { get; set; } = default!;
        protected bool isAuthorizing = true, isAuthorized = false;
        protected override async Task OnInitializedAsync()
        {
            var user = await _authenticationStateProvider.GetAuthenticationStateAsync();
            isAuthorized = user != null && user.User.Identity.IsAuthenticated;
            isAuthorizing = false;
            StateHasChanged();
        }
    }
}
