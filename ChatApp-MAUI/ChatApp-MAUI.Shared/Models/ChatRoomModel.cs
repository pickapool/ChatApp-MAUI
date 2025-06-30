using ChatApp_MAUI.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Models
{
    public class ChatRoomModel
    {
        public string? uid { get; set; }
        public List<string>? members { get; set; }
        public string? Text { get; set; }
        public Enums.MessageType Type { get; set; }
    }
}
