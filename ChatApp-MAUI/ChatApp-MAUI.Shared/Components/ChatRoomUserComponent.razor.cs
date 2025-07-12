using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Components;
namespace ChatApp_MAUI.Shared.Components
{
    public partial class ChatRoomUserComponentBase : ComponentBase
    {
        [Parameter] public ChatRoomModel? ChatRoom { get; set; }
        [Inject] private IUserService _userService { get; set; } = default!;
        [Inject] protected IConversationCallback _conversationCallback { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        protected AuthTokenModel? User;
        protected bool IsLoading = true;
        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            string uid = ChatRoom.members.FirstOrDefault(e => e != _appStateService.User?.Uid)?? string.Empty;
            FilterParameterModel param = new();
            param.IsUid = true;
            param.Uid = uid;
            param.Token = _appStateService.Token;
            User = await _userService.GetUserAccount(param);
            IsLoading = false;
            StateHasChanged();
        }
        protected async Task ShowConversation()
        {
           await _conversationCallback.OnShowConversation(User);
        }
    }
}
