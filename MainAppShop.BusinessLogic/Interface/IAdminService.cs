using MainAppShop.Domain.Entities.User;
using MainAppShop.BusinessLogic.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Interface
{
    public interface IAdminService
    {
        List<Product> GetProducts(string searchTerm = null);
        void CreateProduct(Product product);
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        List<UDbTable> GetAllUsers();
        void DeleteUser(int id);
    }

}
