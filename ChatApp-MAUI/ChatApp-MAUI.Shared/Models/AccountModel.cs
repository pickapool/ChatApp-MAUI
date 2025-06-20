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
        public string? firstname { get; set; }
        public string? lastName { get; set; }
        public string? middleName { get; set; }
        public string? phoneNumber { get; set; }
        public string? displayName { get; set; }
    }
}
