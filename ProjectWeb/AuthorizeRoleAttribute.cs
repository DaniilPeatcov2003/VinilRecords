using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace ProjectWeb.Helpers
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userRole = httpContext.Session["UserRole"]?.ToString();
            return userRole != null && _roles.Contains(userRole);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}
