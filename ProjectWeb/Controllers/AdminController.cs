using MainAppShop.BusinessLogic;
using MainAppShop.BusinessLogic.Core.Admin;
using MainAppShop.Domain.Entities.User;
using ProjectWeb.Extensions;
using ProjectWeb.Filters;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
    public class AdminController : BaseController
    {
        private readonly AdminApi _adminApi;

        public AdminController()
        {
            var bl = new BusinessLogic();
            _adminApi = bl.GetAdminApi();
        }

        public ActionResult Index()
        {
            var userRole = Session["UserRole"]?.ToString();
            if (userRole != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }
        [RoleAuthorize("Admin")]
        public ActionResult Products(string searchTerm = null)
        {
            var products = _adminApi.GetAllProducts(searchTerm);
            return View(products);
        }

        public ActionResult Create()
        {
            return View("~/Views/Admin/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _adminApi.CreateProduct(product);
            return RedirectToAction("Products");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _adminApi.GetProductById(id.Value);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _adminApi.UpdateProduct(product);
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            _adminApi.DeleteProduct(id);
            return RedirectToAction("Products");
        }
        [RoleAuthorize("Admin")]
        public ActionResult UserList()
        {
            var users = _adminApi.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            _adminApi.DeleteUser(id);
            return RedirectToAction("UserList");
        }

        [RoleAuthorize("Admin")]
        public ActionResult AdminDashboard()
        {
            return View("~/Views/Admin/AdminDashboard.cshtml");
        }
    }
}