using ProjectWeb.Models;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using ProjectWeb.Helpers;
using ProjectWeb.Filters;
using System.Web.UI.WebControls;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.BusinessLogic;

namespace ProjectWeb.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProduct _productBL;
        private readonly IUser _userBL;

        public HomeController()
        {
            var bl = new BusinessLogic();
            _productBL = bl.GetProductBl();
            _userBL = bl.GetUserBl();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(string searchQuery)
        {
            return View(_productBL.GetAll(searchQuery));
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Delivery()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var product = _productBL.GetById(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        public ActionResult ProductsForAdmin()
        {
            var products = _productBL.GetAll();
            return View(products);
        }

        public ActionResult UserDashboard()
        {
            ViewBag.Login = User.Identity.Name;
            return View("~/Views/Home/UserDashboard.cshtml");
        }
    }
}