using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Shared.Services.CallBackServices.ChatRoomCallback;
using ChatApp_MAUI.Shared.Services.MessageServices;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace ChatApp_MAUI.Shared.Components
{
    public partial class ChatRoomComponentBase : ComponentBase, IChatRoomCallbackService
    {
        [Inject] protected IChatRoomCallbackService _chatRoomCallbackService { get; set; } = default!;
        [Inject] protected IMessageService _messageService { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        protected List<ChatRoomModel> chatRooms = new();
        protected bool IsLoading;
        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            _chatRoomCallbackService.RegisterCallback(this);
            StateHasChanged();
            if (_appStateService.User.Uid != null)
            {
                await OnShowChatRoom();
            }
        }
        public async Task OnShowChatRoom()
        {
            IsLoading = true;
            StateHasChanged();
            FilterParameterModel param = new();
            param.Token = _appStateService.Token;
            param.Uid = _appStateService.User?.Uid;
            chatRooms = await _messageService.GetChatRooms(param);
            IsLoading = false;
            StateHasChanged();
        }

        public void RegisterCallback(IChatRoomCallbackService listener)
        {
            _chatRoomCallbackService = listener;
        }

        
    }
}
