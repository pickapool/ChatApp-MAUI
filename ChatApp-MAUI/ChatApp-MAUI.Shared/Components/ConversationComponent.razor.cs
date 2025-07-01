using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.CallBackServices.ConversationsCallback;
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
        protected override void OnInitialized()
        {
            _conversationCallback.RegisterCallBack(this);;
        }
        public async Task OnShowConversation(AuthTokenModel user)
        {
            User = user;
            StateHasChanged();
        }

        public void RegisterCallBack(IConversationCallback callback)
        {
            callback = this;
        }

    }
}
