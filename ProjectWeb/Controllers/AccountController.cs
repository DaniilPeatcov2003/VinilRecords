using AutoMapper;
using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.ILogic;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain.Entities.User;
using ProjectWeb.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using ProjectWeb.Filters;
using MainAppShop.Domain.User.Auth;
using System.Web;
using ProjectWeb.Helpers;

namespace ProjectWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext db = new UserContext();

        [HttpGet]
        public ActionResult Dashboard()
        {
            HttpCookie userInfoCookie = Request.Cookies["UserInfo"];
            if (userInfoCookie != null)
            {
                string login = userInfoCookie["Login"];
                string role = userInfoCookie["Role"];

                ViewBag.Login = login;

                if (role == "Admin")
                    return View("~/Views/Home/AdminDashboard.cshtml");
                else if (role == "User")
                    return View("~/Views/Home/UserDashboard.cshtml");
            }

            return RedirectToAction("Login");
        }

        [RoleAuthorize("Admin")]
        public ActionResult AdminDashboard()
        {
            return View("~/Views/Home/AdminDashboard.cshtml");
        }

        [RoleAuthorize("User", "Admin")]
        public ActionResult UserDashboard()
        {
            return View("~/Views/Home/UserDashboard.cshtml");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Введите логин и пароль.";
                return View("~/Views/Home/Login.cshtml");
            }

            using (UserContext db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
                if (user != null)
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        user.Email,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        user.Level.ToString(),
                        FormsAuthentication.FormsCookiePath
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    HttpCookie userCookie = new HttpCookie("UserInfo");
                    userCookie["Login"] = user.Name;
                    userCookie["Role"] = user.Level.ToString();
                    userCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(userCookie);

                    TempData["SuccessMessage"] = "Вы успешно вошли в систему!";
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Неверный логин или пароль.";
            return View("~/Views/Home/Login.cshtml");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Home/Register.cshtml", model);

            var existingUser = db.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже существует.");
                return View("~/Views/Home/Register.cshtml", model);
            }

            var user = new UDbTable
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                LastLogin = DateTime.Now,
                LasIp = Request.UserHostAddress,
                Level = URole.User
            };

            db.Users.Add(user);
            db.SaveChanges();

            var authTicket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                user.Level.ToString()
            );
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);

            HttpCookie userCookie = new HttpCookie("UserInfo");
            userCookie["Login"] = user.Name;
            userCookie["Role"] = user.Level.ToString();
            userCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(userCookie);

            TempData["SuccessMessage"] = "Регистрация прошла успешно!";
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); 
            if (Request.Cookies["UserInfo"] != null)
            {
                var cookie = new HttpCookie("UserInfo");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}