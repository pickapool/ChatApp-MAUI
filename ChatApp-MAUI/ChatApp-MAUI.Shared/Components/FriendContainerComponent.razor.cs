using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Models;
using ChatApp_MAUI.Shared.Services.UserServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class FriendContainerComponentBase : ComponentBase
    {
        [Parameter] public FriendsModel? Friend { get; set; }
        [Inject] protected IUserService _userService { get; set; } = default!;
        protected AuthTokenModel User = new();
        protected override async Task OnInitializedAsync()
        {
            FilterParameterModel param = new();
            param.IsUid = true;
            param.Token = GlobalClass.Token;
            
            if (Friend?.To == GlobalClass.User?.Uid)
            {
                param.Uid = Friend?.From;
                User = await _userService.GetUserAccount(param);
            }
            else
            {
                param.Uid = Friend?.To;
                User = await _userService.GetUserAccount(param);
            }

        }
    }
}
