using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Shared.Services.MessageServices;
using Microsoft.AspNetCore.Components;
using Services.CallBackServices.LoadFriendsCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class ConversationComponentBase : ComponentBase, IConversationCallback
    {
        [Parameter] public AuthTokenModel? User { get; set; }
        [Inject] protected IConversationCallback _conversationCallback { get; set; } = default!;
        [Inject] protected IMessageService _messageService { get; set; } = default!;
        protected List<MessageModel> messages = new();
        protected List<MessageGroup> groupMessages = new();
        protected bool IsLoading = false;
        protected string messageText { get; set; } = string.Empty;
        private string chatRoomId = string.Empty;
        protected override void OnInitialized()
        {
            _conversationCallback.RegisterCallBack(this);
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
            chatRoomId = await _messageService.GetChatRoomId(param);
            param.ChatRoomId = chatRoomId;
            messages = await _messageService.GetMessages(param);
            GroupMessages();
            IsLoading = false;
            StateHasChanged();
        }
        private void GroupMessages()
        {
            groupMessages.Clear();
            foreach (var msg in messages)
            {
                if (!groupMessages.Any())
                {
                    groupMessages.Add(new MessageGroup
                    {
                        SenderId = msg.From,
                        Messages = new List<MessageModel> { msg }
                    });
                    continue;
                }

                var lastGroup = groupMessages.Last();

                if (lastGroup.SenderId == msg.From)
                {
                    lastGroup.Messages.Add(msg);
                }
                else
                {
                    groupMessages.Add(new MessageGroup
                    {
                        SenderId = msg.From,
                        Messages = new List<MessageModel> { msg }
                    });
                }
            }
        }
        public void RegisterCallBack(IConversationCallback callback)
        {
            callback = this;
        }
        protected async Task SendMessage()
        {

            MessageModel message = new();
            message.Text = messageText;
            message.From = GlobalClass.User?.Uid;
            message.To = User?.Uid;
            message.Type = Enums.MessageType.Text;
            message.CreatedAt = DateTime.UtcNow;
            message.ChatRoomId = chatRoomId;
            await _messageService.AddMessage(message, GlobalClass.Token);

            messageText = string.Empty;
        }

    }
}
