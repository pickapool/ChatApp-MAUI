using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.CallBackServices.ConversationsCallback;
using ChatApp_MAUI.Shared.Services.INotificationServices;
using ChatApp_MAUI.Shared.Services.MessageServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class ConversationComponentBase : ComponentBase, IConversationCallback
    {
        [Parameter] public AuthTokenModel? User { get; set; }
        [Inject] protected IConversationCallback _conversationCallback { get; set; } = default!;
        [Inject] protected IMessageService _messageService { get; set; } = default!;
        [Inject] protected IConfiguration _configuration { get; set; } = default!;

        protected List<MessageModel> messages = new();
        protected List<MessageGroup> groupMessages = new();
        protected bool IsLoading = false, IsSending = false;
        protected string messageText { get; set; } = string.Empty;
        private string chatRoomId = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            var hubConnection = NotificationService.GetConnection($"{_configuration["BaseAPI:Url"]}/NotificationHub");
            hubConnection.On<MessageModel>("NotifyMessage", model =>
            {
                if (model.To == User.Uid)
                {
                    messages.Add(model);
                    GroupMessages();
                    StateHasChanged();
                }
            });

            await hubConnection.StartAsync();
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
            IsSending = true;
            MessageModel message = new();
            message.Text = messageText;
            message.From = GlobalClass.User?.Uid;
            message.To = User?.Uid;
            message.Type = Enums.MessageType.Text;
            message.CreatedAt = DateTime.UtcNow;
            message.ChatRoomId = chatRoomId;
            await _messageService.AddMessage(message, GlobalClass.Token);

            messageText = string.Empty;
            IsSending = false;
        }

    }
}
