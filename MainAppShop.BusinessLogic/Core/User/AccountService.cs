using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Core.User
{
    public class AccountService : IAccountService
    {
        private readonly UserContext _context;

        public AccountService(UserContext context)
        {
            _context = context;
        }

        public UDbTable Authenticate(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
        }

        public void RegisterUser(UDbTable user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public UDbTable GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }

        public bool IsAdmin(string login)
        {
            var user = GetUserByName(login);
            return user?.Level == URole.Admin;
        }
    }
}
