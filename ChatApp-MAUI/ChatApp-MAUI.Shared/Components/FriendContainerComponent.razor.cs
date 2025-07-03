using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Components;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class FriendContainerComponentBase : ComponentBase
    {
        [Parameter] public FriendsModel? Friend { get; set; }
        [Inject] protected IUserService _userService { get; set; } = default!;
        [Inject] protected IConversationCallback _conversationCallback { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        protected AuthTokenModel? User;
        protected override async Task OnInitializedAsync()
        {
            FilterParameterModel param = new();
            param.IsUid = true;
            param.Token = _appStateService.Token;
            
            if (Friend?.To == _appStateService.User?.Uid)
            {
                param.Uid = Friend?.From;
                User = await _userService.GetUserAccount(param);
            }
            else
            {
                param.Uid = Friend?.To;
                User = await _userService.GetUserAccount(param);
            }
            StateHasChanged();

        }
        protected async Task GetConversation()
        {
            await _conversationCallback.OnShowConversation(User);
        }
    }
}
