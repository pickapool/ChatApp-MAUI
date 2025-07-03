using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Domain.Entities
{
    public class MessageGroup
    {
        public string? SenderId { get; set; }
        public List<MessageModel> Messages { get; set; } = new();
    }
}
