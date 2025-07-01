using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Shared.Services.MessageServices;
using Microsoft.AspNetCore.Components;
using Services.CallBackServices.LoadFriendsCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class ConversationComponentBase : ComponentBase, IConversationCallback
    {
        [Parameter] public AuthTokenModel? User { get; set; }
        [Inject] protected IConversationCallback _conversationCallback { get; set; } = default!;
        [Inject] protected IMessageService _messageService { get; set; } = default!;
        List<MessageModel> messages = new();
        protected bool IsLoading = false;
        protected override void OnInitialized()
        {
            _conversationCallback.RegisterCallBack(this);;
        }
        public async Task OnShowConversation(AuthTokenModel user)
        {

            IsLoading = true;
            StateHasChanged();
            User = user;
            FilterParameterModel param = new();
            param.Token = GlobalClass.Token;
            param.Uid = GlobalClass.User?.Uid;
            param.SenderUid = user.Uid;

            messages = await _messageService.GetMessages(param);

            IsLoading = false;
            StateHasChanged();
        }

        public void RegisterCallBack(IConversationCallback callback)
        {
            callback = this;
        }

    }
}
