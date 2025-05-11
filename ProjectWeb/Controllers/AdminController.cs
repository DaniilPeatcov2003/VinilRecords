using MainAppShop.Domain.Entities.User;
using MainAppShop.BusinessLogic.DBModel.Seed;
using System;
using System.Net;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProjectWeb.Filters;

namespace ProjectWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserContext db = new UserContext();

        // Только для админа
        [Authorize]
        public ActionResult Index()
        {
            var userRole = Session["UserRole"]?.ToString(); // или localStorage через JS
            if (userRole != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }

        public ActionResult Products(string searchTerm = null)
        {
            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p =>
                    p.Name.Contains(searchTerm) ||
                    p.Artist.Contains(searchTerm));
            }

            return View(products.ToList());
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
            {
                // Временный вывод ошибок в консоль/отладчик
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }

                return View(product);
            }

            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Products");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = db.Products.Find(id);
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
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        [RoleAuthorize("Admin")]
        public ActionResult AdminDashboard()
        {
            return View("~/Views/Admin/AdminDashboard.cshtml");
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("UserList");
        }


        public ActionResult UserList()
        {
            var users = db.Users.ToList();
            return View(users);
        }
    }
}