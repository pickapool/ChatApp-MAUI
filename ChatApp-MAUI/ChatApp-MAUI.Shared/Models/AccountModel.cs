using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
            datecreated = DateTime.Now;
        }
        public string? uid { get; set; }
        public DateTime? datecreated { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
    }
}
