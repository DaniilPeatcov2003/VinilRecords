using ProjectWeb.Models;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            var products = new List<Models.Product>
            {
              new Product { Id = 1, Name = "Гранатовый альбом", Artist = "Сплин", ImageUrl = "/Content/Images/album1.jpg", Price = 20 },
              new Product { Id = 2, Name = "Горизонт событий", Artist = "Би-2", ImageUrl = "/Content/Images/album2.jpg", Price = 25 },
              new Product { Id = 3, Name = "Всё что вокруг", Artist = "Нервы", ImageUrl = "/Content/Images/album3.jpg", Price = 18 },
              new Product { Id = 4, Name = "Земфира", Artist = "Земфира", ImageUrl = "/Content/Images/album4.jpg", Price = 22 },
              new Product { Id = 5, Name = "Выход в город", Artist = "Noize MC", ImageUrl = "/Content/Images/album5.jpg", Price = 25 },
              new Product { Id = 6, Name = "Hard Reboot 3.0", Artist = "Noize MC", ImageUrl = "/Content/Images/album6.jpg", Price = 25 },
              new Product { Id = 7, Name = "Hattori", Artist = "Miyagi & Эндшпиль", ImageUrl = "/Content/Images/album7.png", Price = 25 },
              new Product { Id = 8, Name = "Трёхмерные рифмы", Artist = "Каста", ImageUrl = "/Content/Images/album8.png", Price = 25 },
              new Product { Id = 9, Name = "Здравствуй, Это Я…", Artist = "Руки Вверх", ImageUrl = "/Content/Images/album9.png", Price = 30 },
              new Product { Id = 10, Name = "Босоногий мальчик", Artist = "Леонид Агутин", ImageUrl = "/Content/Images/album10.png", Price = 25 },
              new Product { Id = 11, Name = "Небы", Artist = "Ёлка", ImageUrl = "/Content/Images/album11.png", Price = 25 },
              new Product { Id = 12, Name = "Солнце", Artist = "Ани Лорак", ImageUrl = "/Content/Images/album12.png", Price = 25 }
            };

            return View(products);
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

        private UserContext db = new UserContext();

        [HttpPost]
        public ActionResult Register(string name, string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new UDbTable
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    LastLogin = DateTime.Now,
                    LasIp = Request.UserHostAddress,
                    Level = URole.User
                };

                db.Users.Add(user);
                db.SaveChanges();

                // Можно сразу авторизовать и перенаправить
                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                user.LasIp = Request.UserHostAddress;
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Неверный email или пароль.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}