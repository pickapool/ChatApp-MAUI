using ChatApp_MAUI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Common
{
    public class GlobalClass
    {
        public static AuthTokenModel? User { get; set; } = new();
        public static string Token { get; set; } = string.Empty;
        public static bool IsDarkMode;
    }
}
