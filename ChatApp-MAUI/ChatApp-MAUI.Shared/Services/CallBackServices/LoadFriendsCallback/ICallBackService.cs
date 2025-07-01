using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CallBackServices.LoadFriendsCallback
{
    public interface ICallBackService
    {
        Task OnShowFrieds();
        void RegisterCallback(ICallBackService listener);

    }
}
