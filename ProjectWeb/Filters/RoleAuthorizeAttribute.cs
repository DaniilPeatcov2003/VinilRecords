using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWeb.Extensions;

namespace ProjectWeb.Filters
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            this.allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var roleCookie = httpContext.Request.Cookies["TWEB-D"];
            if (roleCookie != null)
            {
                string role = roleCookie["Role"];
                return allowedRoles.Contains(role);
            }
            return false;
        }
    }
}