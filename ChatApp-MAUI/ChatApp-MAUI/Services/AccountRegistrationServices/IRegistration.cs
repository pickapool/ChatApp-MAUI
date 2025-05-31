using ChatApp_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Services.AccountRegistrationServices
{
    public interface IRegistration
    {
        Task<string> RegisterAsync(AccountModel account);
    }
}
