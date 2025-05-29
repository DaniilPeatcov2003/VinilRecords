using MainAppShop.BusinessLogic.BLogic;
using MainAppShop.BusinessLogic.Core.Admin;
using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.BusinessLogic.ILogic;
using MainAppShop.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic
{
    public class BusinessLogic
    {
        public AdminApi GetAdminApi()
        {
            return new AdminApi();
        }

        public ICart GetAllProducts()
        {
            return new CartBl();
        }

        public ICart GetProductById()
        {
            return new CartBl();
        }

        public IAccountService GetAccountBl()
        {
            var context = new UserContext();
            return new AccountService(context);
        }

        public IUser GetUserBl()
        {
            return new UserBL();
        }
        public IProduct GetProductBl()
        {
            return new ProductBl();
        }

        public IPayment GetPaymentBl()
        {
            return new PaymentBL();
        }
    }
}
