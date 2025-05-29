using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Interface
{
    public interface ICart
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
    }
}
