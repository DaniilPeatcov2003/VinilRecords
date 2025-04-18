﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectWeb.Models;

namespace ProjectWeb.Controllers
{
    public class CartController : Controller
    {
        private static readonly List<Product> products = new List<Product>
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

        public ActionResult Index()
        {
            ViewBag.Products = products;
            return View();
        }
        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return HttpNotFound();

            return View("Details", product); // Явно указываем имя представления
        }
        public JsonResult GetProducts()
        {
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}

