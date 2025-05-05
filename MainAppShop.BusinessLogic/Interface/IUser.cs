using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.Domain.User.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Interface
{
    public interface IUser
    {
        string AuthentificateUser(UserAuthAction auth);
        int GetUserIdBySessionKey(string sessionKey);
        bool IsSessionValid(string key);
        string RegisterUserAction(ULoginData data);
    }
}