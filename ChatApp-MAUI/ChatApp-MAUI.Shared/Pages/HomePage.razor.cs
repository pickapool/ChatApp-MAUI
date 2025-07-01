using ChatApp_MAUI.Shared.Services.NavigationServices;
using ChatApp_MAUI.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.UserServices;
using ChatApp_MAUI.Shared.Services.FriendServices;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class HomePageBase : ComponentBase
    {
        [Inject] protected IFriendService _friendService { get; set; } = default!;
        List<FriendsModel> friends = new();
        protected override async Task OnInitializedAsync()
        {
            friends = await _friendService.GetFriends(GlobalClass.User.Uid, GlobalClass.Token);
        }
    }
}
