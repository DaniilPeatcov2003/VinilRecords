using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using ProjectWeb.App_Start;
using System.Security.Principal;

namespace ProjectWeb
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var mapper = AutoMapperConfig.Initialize();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Cart"] = new List<ProjectWeb.Models.Product>();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = new string[] { };
                GenericPrincipal userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
                HttpContext.Current.User = userPrincipal;
            }
        }
    }
}