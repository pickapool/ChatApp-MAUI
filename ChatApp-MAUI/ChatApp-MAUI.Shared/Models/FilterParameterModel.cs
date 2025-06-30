using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Models
{
    public class FilterParameterModel
    {
        public string? Token { get; set; }
        public bool IsName { get; set; }
        public string? Name { get; set; }
        public bool IsUid { get; set; }
        public string? Uid { get; set; }
    }
}
