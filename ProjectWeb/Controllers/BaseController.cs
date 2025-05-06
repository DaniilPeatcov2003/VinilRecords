using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Проверка куки UserInfo
            var cookie = HttpContext.Request.Cookies["UserInfo"];
            if (cookie != null)
            {
                ViewBag.Login = cookie["Login"];
                ViewBag.Role = cookie["Role"];
                ViewBag.IsLoggedIn = true;
            }
            else
            {
                ViewBag.IsLoggedIn = false;
            }

            // Проверка на админа (по имени из куки, если есть)
            string login = cookie?["Login"];
            bool isAdmin = false;

            if (!string.IsNullOrEmpty(login))
            {
                using (var db = new UserContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Name == login);
                    if (user != null && user.Level == URole.Admin)
                    {
                        isAdmin = true;
                    }
                }
            }

            ViewBag.IsAdmin = isAdmin;

            base.OnActionExecuting(filterContext);
        }
    }
}