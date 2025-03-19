using MainAppShop.BusinessLogic;
using MainAppShop.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
	public class AuthController : Controller
	{
        private readonly IUser _user;
        public AuthController()
        {
            var bl = new BusinessLogic();
            _user = bl.GetUserBl();
        }
        public ActionResult Index()
        {
            string key = "abcd";
            bool isValid = _user.IsSessionValid(key);
            return View();
        }
    }
}