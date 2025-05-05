using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index() => View();

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            // Пример простой авторизации
            if (username == "admin" && password == "1234")
            {
                // Создаем cookie
                HttpCookie authCookie = new HttpCookie("AuthCookie");
                authCookie.Values["Username"] = username;
                authCookie.Values["Role"] = "Administrator";
                authCookie.Expires = DateTime.Now.AddMinutes(30);

                Response.Cookies.Add(authCookie);
                authCookie.HttpOnly = true;
                authCookie.Secure = Request.IsSecureConnection;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Неверный логин или пароль";
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            if (Request.Cookies["AuthCookie"] != null)
            {
                var cookie = new HttpCookie("AuthCookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Login");
        }
    }
}