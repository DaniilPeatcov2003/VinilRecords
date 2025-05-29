using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain.Entities.User;
using MainAppShop.Domain.User.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MainAppShop.BusinessLogic.Interface
{
    public interface IUser
    {
        bool AuthentificateUser(UDbTable user);
        int GetUserIdBySessionKey(string sessionKey);
        bool IsSessionValid(string key);
        bool Register(UDbTable data);
        HttpCookie GenCookie(string email);
        UserMinimal GetUserByCookie(string httpCookieValue);
        bool SignOut(string httpCookieValue);
        string GetUserRole(string email);
        string GetUserName(string email);
        UDbTable GetUserData(string email);
    }
}