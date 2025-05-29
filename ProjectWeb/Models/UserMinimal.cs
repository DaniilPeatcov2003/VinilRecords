using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace ProjectWeb.Models
{
    public class UserMinimal
    {
        public UserMinimal(MainAppShop.Domain.Entities.User.UserMinimal u)
        {
            if (u == null)
            {
                IsAuthenticated = false;
                return;
            }
            Name = u.Name;
            Email = u.Email;
            Level = u.Level;
            if (!Email.IsEmpty())
            {
                IsAuthenticated = true;
            }
        }

        protected UserMinimal()
        {
            IsAuthenticated = false;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public URole Level { get; set; }

        public bool IsAuthenticated { get; set; } = false;
    }
}