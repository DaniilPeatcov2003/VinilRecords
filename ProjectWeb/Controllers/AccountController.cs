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
using MainAppShop.BusinessLogic;
using ProjectWeb.Extensions;

namespace ProjectWeb.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUser _userBL;

        public AccountController()
        {
            var bl = new BusinessLogic();
            _userBL = bl.GetUserBl();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            HttpCookie userInfoCookie = Request.Cookies["TWEB-D"];
            if (userInfoCookie != null)
            {
                string login = userInfoCookie["Login"];
                string role = userInfoCookie["Role"];

                ViewBag.Login = login;

                if (role == "Admin")
                    return View("~/Views/Admin/AdminDashboard.cshtml");
                else if (role == "User")
                    return View("~/Views/Home/UserDashboard.cshtml");
            }

            return RedirectToAction("Login");
        }

        [RoleAuthorize("User")]
        public ActionResult UserDashboard()
        {
            ViewBag.Login = User.Identity.Name;
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

            var user = new UDbTable()
            {
                Email = login,
                Password = password,
            };

            if (!_userBL.AuthentificateUser(user))
            {
                ViewBag.Error = "Неверный логин или пароль.";
                return View("~/Views/Home/Login.cshtml");
            }

            var role = _userBL.GetUserRole(login);
            var name = _userBL.GetUserName(login); 

            var authTicket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                role,
                FormsAuthentication.FormsCookiePath
            );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);

            HttpCookie userCookie = new HttpCookie("TWEB-D");
            userCookie["Login"] = name;
            userCookie["Role"] = role;
            userCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(userCookie);

            TempData["SuccessMessage"] = "Вы успешно вошли в систему!";
            return RedirectToAction("Index", "Home");
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

            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, UDbTable>());
            var mapper = config.CreateMapper();
            var udb = mapper.Map<UDbTable>(model);

            if(_userBL.Register(udb))
            { 
                TempData["SuccessMessage"] = "Регистрация прошла успешно!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("~/Views/Home/Register.cshtml", model);
            }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); 
            if (Request.Cookies["TWEB-D"] != null)
            {
                var cookie = new HttpCookie("TWEB-D");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}