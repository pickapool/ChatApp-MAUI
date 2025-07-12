using ChatApp_MAUI.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using MudBlazor;
using ChatApp_MAUI.Domain.Common;
using ChatApp_MAUI.Infrastructure.Services.MessageServices;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.INotificationServices;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class ConversationComponentBase : ComponentBase, IConversationCallback
    {
        [Parameter] public AuthTokenModel? User { get; set; }
        [Inject] protected IConversationCallback _conversationCallback { get; set; } = default!;
        [Inject] protected IMessageService _messageService { get; set; } = default!;
        [Inject] protected IConfiguration _configuration { get; set; } = default!;
        [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;

        protected List<MessageModel> messages = new();
        protected List<MessageGroup> groupMessages = new();
        protected bool IsLoading = false, IsSending = false;
        protected string messageText { get; set; } = string.Empty;
        private string chatRoomId = string.Empty;
        protected MudTextField<string> textField;
        protected override async Task OnInitializedAsync()
        {
            var hubConnection = NotificationService.GetConnection($"{_configuration["BaseAPI:Url"]}/NotificationHub");
            hubConnection.On<MessageModel>("NotifyMessage", async (model) =>
            {
                var myUid = _appStateService.User.Uid;
                var openChatUid = User.Uid;

                if ((myUid == model.To || myUid == model.From) &&
                    (model.From == openChatUid || model.To == openChatUid))
                {
                    messages.Add(model);
                    await InvokeAsync(GroupMessages);
                }
            });

            await hubConnection.StartAsync();
            _conversationCallback.RegisterCallBack(this);
        }
        public async Task OnShowConversation(AuthTokenModel user)
        {

            IsLoading = true;
            await InvokeAsync(StateHasChanged);
            User = user;
            FilterParameterModel param = new();
            param.Token = _appStateService.Token;
            param.Uid = _appStateService.User?.Uid;
            param.SenderUid = user.Uid;
            chatRoomId = await _messageService.GetChatRoomId(param);
            param.ChatRoomId = chatRoomId;
            messages = await _messageService.GetMessages(param);
            await InvokeAsync(GroupMessages);

        }
        private async Task GroupMessages()
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
            IsLoading = false;
            await InvokeAsync(StateHasChanged);
            await Task.Delay(500);
            await _jsRuntime.InvokeVoidAsync("scrollToBottom");
            await textField.FocusAsync();
        }
        public void RegisterCallBack(IConversationCallback callback)
        {
            callback = this;
        }
        protected async Task SendMessage()
        {
            await InvokeAsync(StateHasChanged);
            if (String.IsNullOrEmpty(messageText))
            {
                await textField.FocusAsync();
                return;
            }
            else
            {
                IsSending = true;
                MessageModel message = new();
                message.Text = messageText;
                message.From = _appStateService.User?.Uid;
                message.To = User?.Uid;
                message.Type = Enums.MessageType.Text;
                message.CreatedAt = DateTime.UtcNow;
                message.ChatRoomId = chatRoomId;
                await _messageService.AddMessage(message, _appStateService.Token);
                IsSending = false;
            }
            await textField.Clear();
            await textField.FocusAsync();
        }
        protected async Task HandleKeyUp(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && !IsSending)
            {
                await SendMessage();
            }
        }
    }
}
