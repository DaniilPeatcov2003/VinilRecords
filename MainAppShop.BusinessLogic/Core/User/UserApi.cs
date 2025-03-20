using MainAppShop.Domain;
using MainAppShop.Domain.User.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Core.User
{
    public class UserApi
    {
        public UserApi() { }

        public bool IsSessionValidAction(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return true;
        }

        public bool IsProductValidAction(int id)
        {
            return true;
        }

        public string AuthentificateUserAction(UserAuthAction auth)
        {
            return "";
        }

        internal int GetUserIdBySessionKeyAction(string sessionKey)
        {
            return 1;
        }

        internal ReceiptToPay GetReceiptToPayByUserIdAction(int uId)
        {
            return new ReceiptToPay();
        }

    }
}