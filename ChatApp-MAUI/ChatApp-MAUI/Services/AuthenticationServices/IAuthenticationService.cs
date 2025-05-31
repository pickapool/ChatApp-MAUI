using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(string email, string password);
    }
}
