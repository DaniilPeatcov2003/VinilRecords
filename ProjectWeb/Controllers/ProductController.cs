using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using MainAppShop.BusinessLogic;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.BusinessLogic.Interface;
using ProjectWeb.Models;

namespace ProjectWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        public ProductController() {
            var bl = new BusinessLogic();
            _product = bl.GetProductBl();
        }
    }
}

