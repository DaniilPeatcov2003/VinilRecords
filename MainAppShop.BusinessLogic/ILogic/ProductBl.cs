using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.ILogic
{
    public class ProductBl : UserApi, IProduct
    {
        public bool IsProductValid(int id)
        {
            return IsProductValidAction(id);
        }
    }
}
