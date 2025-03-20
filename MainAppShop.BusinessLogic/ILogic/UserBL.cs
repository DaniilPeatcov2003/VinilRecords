using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain.User.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.ILogic
{
    public class UserBL : UserApi, IUser
    {
        public string AuthentificateUser(UserAuthAction auth)
        {
            return AuthentificateUserAction(auth);
        }

        public int GetUserIdBySessionKey(string sessionKey)
        {
            return GetUserIdBySessionKeyAction(sessionKey);
        }

        public bool IsSessionValid(string key)
        {
            return IsSessionValidAction(key);
        }
    }
}
