using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Interface
{
    public interface IAccountService
    {
        UDbTable Authenticate(string username, string password);
        void RegisterUser(UDbTable user);
        UDbTable GetUserByName(string name);
        bool IsAdmin(string login);
    }
}
