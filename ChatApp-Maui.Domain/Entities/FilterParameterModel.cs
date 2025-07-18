﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Domain.Entities
{
    public class FilterParameterModel
    {
        public string? Token { get; set; }
        public bool IsName { get; set; }
        public string? Name { get; set; } = string.Empty;
        public bool IsUid { get; set; }
        public string? Uid { get; set; }
        public string? SenderUid { get; set; }
        public string? ChatRoomId { get; set; }
    }
}
