using ChatApp_MAUI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.CallBackServices.ConversationsCallback
{
    public interface IConversationCallback
    {
        void RegisterCallBack(IConversationCallback callback);
        Task OnShowConversation(AuthTokenModel user);
    }
}
