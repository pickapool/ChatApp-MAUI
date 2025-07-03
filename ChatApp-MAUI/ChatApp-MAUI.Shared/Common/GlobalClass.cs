using ChatApp_MAUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Common
{
    public class AppStateService
    {
        public AuthTokenModel? User { get; set; } = new();
        public string Token { get; set; } = string.Empty;
        public bool IsDarkMode;
    }
}
