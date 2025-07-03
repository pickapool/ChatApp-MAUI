using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Infrastructure
{
    public class HttpErrorHelper
    {
        public static string GetHttpError(string responseValue)
        {
            if (responseValue.ToLower().Contains("email_exist"))
                return "Email already exist.";
            if (responseValue.ToLower().Contains("invalid email"))
                return "Invalid email address.";
            return responseValue;
        }
    }
}
