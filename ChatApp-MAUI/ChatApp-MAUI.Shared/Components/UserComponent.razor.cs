using ChatApp_MAUI.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Components
{
    public partial class UserComponentBase : ComponentBase
    {
        [Parameter] public AuthTokenModel? User { get; set; }

    }
}
