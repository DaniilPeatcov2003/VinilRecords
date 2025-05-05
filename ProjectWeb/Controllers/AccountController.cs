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
using MainAppShop.Domain.User.Auth;
using System.Web;

namespace ProjectWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext db = new UserContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Введите логин и пароль.";
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                TempData["SuccessMessage"] = "Вы успешно вошли в систему!"; ;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Неверный логин или пароль.";
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string login, string password, string name)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Введите логин и пароль.";
                return View();
            }

            var userExists = db.Users.Any(u => u.Email == login);
            if (userExists)
            {
                ViewBag.Error = "Пользователь уже существует.";
                return View();
            }

            var newUser = new UDbTable { Email = login, Password = password, Level = URole.User };
            db.Users.Add(newUser);
            db.SaveChanges();

            FormsAuthentication.SetAuthCookie(newUser.Email, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}