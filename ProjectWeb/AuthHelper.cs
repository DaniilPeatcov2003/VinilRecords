using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWeb.Helpers
{
    public static class AuthHelper
    {
        private const string CookieName = "AuthCookie";

        public static UDbTable AuthenticateUser(string email, string password)
        {
            using (var db = new UserContext())
            {
                return db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            }
        }

        public static HttpCookie GenCookie(string username)
        {
            var cookie = new HttpCookie(CookieName, username)
            {
                Expires = DateTime.Now.AddMinutes(30),
                HttpOnly = true
            };
            return cookie;
        }

        public static string ReadCookie(HttpRequestBase request)
        {
            var cookie = request.Cookies[CookieName];
            return cookie?.Value;
        }

        public static void RemoveCookie(HttpResponseBase response)
        {
            var expiredCookie = new HttpCookie(CookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            response.Cookies.Add(expiredCookie);
        }

        public static bool IsAuthenticated(HttpRequestBase request)
        {
            return !string.IsNullOrEmpty(ReadCookie(request));
        }
    }

}