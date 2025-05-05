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
            ViewBag.IsLoggedIn = User.Identity.IsAuthenticated;
            ViewBag.Login = User.Identity.IsAuthenticated ? User.Identity.Name : "";
            base.OnActionExecuting(filterContext);
        }
    }
}