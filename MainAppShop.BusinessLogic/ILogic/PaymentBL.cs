using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain;
using MainAppShop.Domain.User.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.BLogic
{
    public class PaymentBL : UserApi, IPayment
    {
        public ReceiptToPay GetReceiptToPayByUserId(int uId)
        {
            return GetReceiptToPayByUserIdAction(uId);
        }
    }
}