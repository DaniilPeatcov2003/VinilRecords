using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain.Entities.User;
using MainAppShop.Domain.User.Auth;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MainAppShop.BusinessLogic.ILogic
{
    public class UserBL : UserApi ,IUser
    {
        public bool AuthentificateUser(UDbTable user)
        {
            return AuthentificateUserAction(user);
        }

        public int GetUserIdBySessionKey(string sessionKey)
        {
            return GetUserIdBySessionKeyAction(sessionKey);
        }

        public bool IsSessionValid(string key)
        {
            return IsSessionValidAction(key);
        }

        public bool Register(UDbTable data)
        {
            return RegisterUserAction(data);
        }

        public HttpCookie GenCookie(string email)
        {
            return Cookie(email);
        }

        public UserMinimal GetUserByCookie(string httpCookieValue)
        {
            return UserCookie(httpCookieValue);
        }

        public bool SignOut(string httpCookieValue)
        {
            return SignOutAction(httpCookieValue);
        }

        public UDbTable GetUserData(string email)
        {
            using (var db = new UserContext())
            {
                return db.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public string GetUserRole(string email)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                return user != null ? user.Level.ToString() : "User"; // по умолчанию "User"
            }
        }

        public string GetUserName(string email)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                return user != null ? user.Name : "";
            }
        }
    }
}