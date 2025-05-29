using System.Collections.Generic;
using System.Web.Mvc;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.BusinessLogic;
using ProjectWeb.Models;

namespace ProjectWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly IProduct _productLogic;

        public CartController()
        {
            var bl = new BusinessLogic();
            _productLogic = bl.GetProductBl();
        }

        public ActionResult Index()
        {
            var products = _productLogic.GetAll();
            var viewModelList = MapToViewModels(products);
            ViewBag.Products = viewModelList;
            return View();
        }

        public ActionResult Details(int id)
        {
            if (!_productLogic.IsProductValid(id))
                return HttpNotFound();

            var product = _productLogic.GetById(id);
            var viewModel = MapToViewModel(product);
            return View(viewModel);
        }

        public JsonResult GetProducts()
        {
            var products = _productLogic.GetAll();
            var viewModelList = MapToViewModels(products);
            return Json(viewModelList, JsonRequestBehavior.AllowGet);
        }

        private List<Product> MapToViewModels(List<MainAppShop.Domain.Entities.User.Product> products)
        {
            var result = new List<Product>();
            foreach (var p in products)
            {
                result.Add(MapToViewModel(p));
            }
            return result;
        }

        private Product MapToViewModel(MainAppShop.Domain.Entities.User.Product p)
        {
            return new Product
            {
                Id = p.Id,
                Name = p.Name,
                Artist = p.Artist,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Description = p.Description
            };
        }
    }
}