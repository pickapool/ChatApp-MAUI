using ChatApp_MAUI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Services.UserServices
{
    public interface IUserService
    {
        Task<List<AuthTokenModel>> SearchUsers(FilterParameterModel param);
    }
}
