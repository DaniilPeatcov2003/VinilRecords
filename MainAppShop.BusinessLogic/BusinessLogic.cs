using MainAppShop.BusinessLogic.BLogic;
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
