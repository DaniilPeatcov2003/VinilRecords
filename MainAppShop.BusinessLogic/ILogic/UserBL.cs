using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.ILogic
{
    public class UserBL : UserApi, IUser
    {
        public bool IsSessionValid(string key)
        {
            return IsSessionValidAction(key);
        }
    }
}
