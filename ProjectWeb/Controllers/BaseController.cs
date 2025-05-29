using MainAppShop.BusinessLogic;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain.Entities.User;
using ProjectWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUser _session;

        public BaseController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetUserBl();
        }

        public void DestroySession()
        {
            var httpCookie = Request.Cookies["TWEB-D"];

            if (httpCookie != null)
            {
                var user = _session.GetUserByCookie(httpCookie.Value);
                if (user != null)
                {
                    _session.SignOut(httpCookie.Value);
                }
            }

            System.Web.HttpContext.Current.Session.Clear();

            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("TWEB-D"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["TWEB-D"];
                if (cookie != null)
                {
                    cookie.Expires = System.DateTime.Now.AddDays(-1);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }
        }

        public void SessionStatus()
        {
            var httpCookie = Request.Cookies["TWEB-D"];
            if (httpCookie != null)
            {
                var user = _session.GetUserByCookie(httpCookie.Value);

                if (user != null)
                {
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "true";
                    System.Web.HttpContext.Current.SetMySessionObject(user);
                    return;
                }

                System.Web.HttpContext.Current.Session.Clear();

                if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("TWEB-D")) DestroySession();
            }

            System.Web.HttpContext.Current.Session["LoginStatus"] = "false";

            return;
        }}
    }