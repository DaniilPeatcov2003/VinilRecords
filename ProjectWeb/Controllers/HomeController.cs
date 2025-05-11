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

namespace ProjectWeb.Controllers
{
    public class HomeController : BaseController
    {
        private UserContext db = new UserContext();
        public ActionResult Index()
        {
            // Установка cookie
            var cookie = new HttpCookie("UserId", "12345");
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);

            // Чтение cookie
            var userIdCookie = Request.Cookies["UserId"];
            if (userIdCookie != null)
            {
                string userId = userIdCookie.Value;
            }

            return View();
        }


        public ActionResult Products(string searchQuery)
        {
            // Если в базе нет товаров, добавим их
            if (!db.Products.Any())
            {

                var newProducts = new List<MainAppShop.Domain.Entities.User.Product>
                {
                new MainAppShop.Domain.Entities.User.Product { Id = 1, Name = "Гранатовый альбом", Artist = "Сплин", ImageUrl = "/Content/Images/album1.jpg", Price = 20, Description = "«Гранатовый альбом» — четвёртый студийный альбом российской рок-группы «Сплин», вышедший 1 апреля 1998 года; после него группа приобрела популярность по всей России. Альбом записан в феврале-марте 1998 года в студии «SBI Records». Список композиций: 1.\t«Весь этот бред»\t\n2.\t«Достань гранату»\t\n3.\t«Орбит без сахара»\t\n4.\t«Приходи»\t\n5.\t«Свет горел всю ночь»\t\n6.\t«Люся сидит дома»\t\n7.\t«Бог устал нас любить»\t\n8.\t«Катись, колесо!»\t\n9.\t«Выхода нет»\t\n10.\t«Коктейли третьей мировой»\t\n11.\t«Джим»\t\n12.\t«Мария и Хуана»" },
                new MainAppShop.Domain.Entities.User.Product { Id = 2, Name = "Горизонт событий", Artist = "Би-2", ImageUrl = "/Content/Images/album2.jpg", Price = 25, Description = "«Горизонт событий» — десятый студийный альбом российской рок-группы «Би-2», вышедший в 2017 году. Список композиций: 1.\t«Лётчик»\r\n2.\t«Чёрное солнце»\r\n3.\t«Тема века»\r\n4.\t«Детство»\r\n5.\t«Философский камень»\r\n6.\t«Лайки»\r\n7.\t«Виски» (feat. Джон Грант)\r\n8.\t«Пора возвращаться домой» (feat. Oxxxymiron)\r\n9.\t«Пофигу»\r\n10.\t«Алиса»\r\n11.\t«Родина»"},
                new MainAppShop.Domain.Entities.User.Product { Id = 3, Name = "Всё что вокруг", Artist = "Нервы", ImageUrl = "/Content/Images/album3.jpg", Price = 18, Description = "«Всё, что вокруг» — дебютный альбом рок-группы «Нервы», выпущенный 1 января 2012 года. В альбом вошло 18 композиций в стиле подросткового пост-панка и поп-рока. Список композиций: 1. \r\nНервы\r\n2. \r\nБей моё сердце\r\n3. \r\nА А А\r\n4. \r\nБатареи \r\n5. \r\nСлишком влюблён\r\n6. \r\nКурим \r\n7. \r\nВолшебная\r\n8. \r\nБудем друзьями\r\n9. \r\nГлупая \r\n10. \r\nАватар \r\n11. \r\nБудет легче \r\n12. \r\nВклочья \r\n13. \r\nЯ её люблю \r\n14. \r\nКофе мой друг \r\n15. \r\nМуза \r\n16. \r\nЕё имя \r\n17. \r\nMy Lady \r\n18. \r\nНечего терять"},
                new MainAppShop.Domain.Entities.User.Product { Id = 4, Name = "Земфира", Artist = "Земфира", ImageUrl = "/Content/Images/album4.jpg", Price = 22, Description = "«Земфира» — первый студийный альбом Земфиры, записанный в составе её группы «Zемфира» и выпущенный 10 мая 1999 года на лейбле REAL Records. Список композиций: 1.\t«Почему»\r\n2.\t«Снег»\r\n3.\t«Синоптик»\r\n4.\t«Ромашки»\r\n5.\t«Маечки»\r\n6.\t«СПИД»\r\n7.\t«Румба»\r\n8.\t«Скандал»\r\n9.\t«Непошлое»\r\n10.\t«Припевочка»\r\n11.\t«–140»\r\n12.\t«Ариведерчи»\r\n13.\t«Ракеты»\r\n14.\t«Земфира»" },
                new MainAppShop.Domain.Entities.User.Product { Id = 5, Name = "Выход в город", Artist = "Noize MC", ImageUrl = "/Content/Images/album5.jpg", Price = 25, Description = "«Выход в город» — девятый студийный альбом российского рэп-рок-исполнителя Noize MC. Первая часть альбома была выпущена 19 ноября 2021 года и состояла из 10 треков. Полная версия альбома из 20 песен, также включающая вторую часть релиза, была выпущена 17 декабря 2021 года с новой обложкой. Автором обложки стал современный российский художник Дима Ребус, работающий с акварелью, дождевой водой и химическими растворами. Список композиций: 1.\t«Вояджер-1»\r\n2.\t«Выход в город»\r\n3.\t«Миокард»\r\n4.\t«Сельма Лагерлёф»\r\n5.\t«Сопротивление воздуха»\r\n6.\t«Двадцатые годы»\r\n7.\t«Век-волкодав» (трибьют Осипу Мандельштаму)\r\n8.\t«Столетняя война»\r\n9.\t«Вуду»\r\n10.\t«Инстинкт»\r\n11.\t«Трансгуманизм 2.0» (при уч. White Punk)\r\n12.\t«Паучьими тенётами»\r\n13.\t«Песня предателя»\r\n14.\t«Бизнесмен, что продал мир»\r\n15.\t«Всё как у людей» (трибьют Егору Летову)\r\n16.\t«Букет крапивы»\r\n17.\t«26.04»\r\n18.\t«Первые симптомы»\r\n19.\t«Опыт отсутствия»\r\n20.\t«Вереница» (при уч. ВЕРЕНИЦА)" },
                new MainAppShop.Domain.Entities.User.Product { Id = 6, Name = "Hard Reboot 3.0", Artist = "Noize MC", ImageUrl = "/Content/Images/album6.jpg", Price = 25, Description = "Hard Reboot — шестой студийный альбом российского рэп-исполнителя Noize MC, интернет-премьера состоялась на интернет-ресурсе Яндекс.Музыка 9 сентября 2014 года (официальной датой выхода считается 10 сентября). Презентация альбома состоялась 15 ноября в 20:00 на сцене Главклуба. Помимо русскоязычных композиций содержит совместный с Astronautalis трек на английском языке («Hard Reboot»). Также в альбоме присутствуют совместные треки с поэтессами Верой Полозковой (трек «абв&эюя») и Мариной Кацубой (трек «М»). Список композиций: 1.\t«Роботы»\r\n2.\t«Ne2Da?» (совместно с Mewark)\r\n3.\t«Фарыфуры»\r\n4.\t«Говорящие головы»\r\n5.\t«Хозяин леса»\r\n6.\t«Сгораю»\r\n7.\t«Сохрани мою речь» (OST «Сохрани мою речь навсегда»)\r\n8.\t«Come $ome All (Тоталитарный трэпъ)»\r\n9.\t«абв&эюя» (совместно с Верой Полозковой)\r\n10.\t«Старые шрамы»\r\n11.\t«Снайпер»\r\n12.\t«220»\r\n13.\t«М» (совместно с Мариной Кацуба)\r\n14.\t«Hard Reboot» (совместно с Astronautalis)\r\n15.\t«Safe Mode» (Инструментальная версия)\t " },
                new MainAppShop.Domain.Entities.User.Product { Id = 7, Name = "Hattori", Artist = "Miyagi & Эндшпиль", ImageUrl = "/Content/Images/album7.png", Price = 25, Description = "HATTORI — долгожданное возвращение Miyagi и Эндшпиля в музыкальную индустрию после восьмимесячного перерыва.\r\n\r\nНазвание мини-альбома отсылает нас к легендарному японскому самураю времён Сэнгоку — Хаттори Хандзо, цуба чьей катаны изображена на обложке. Примечательно, что исполнителем указан старый псевдоним Сослана — Эндшпиль, хотя последние четыре года тот выступал как Andy Panda. Список композиций: 1. \r\nSaloon\r\n2. \r\nНочь \r\n3. \r\nВременно \r\n4. \r\nНе теряя\r\n5. \r\nNeed Me \r\n6. \r\nSilhouette " },
                new MainAppShop.Domain.Entities.User.Product { Id = 8, Name = "Трёхмерные рифмы", Artist = "Каста", ImageUrl = "/Content/Images/album8.png", Price = 25, Description = "«Трёхмерные рифмы» — дебютный студийный альбом рэп-объединения «Объединённая Каста», выпущенный музыкальным издательством Duncan Records на аудиокассетах 2 июля 1999 года. Список композиций: 1. \tЛучшие традиции хип-хопа (скит) \r\n2. \tТрёхмерные рифмы \r\n3. \tУ подножия горы\r\n4. \tМы берём это на улицах \r\n5. \tСказка о песочных часах (1998)\r\n6. \tПесня без темы \r\n7. \tГорячие трубы… \r\n8. \tСвободный стиль \r\n9. \tСколько воинов… \r\n10. \tВирус \r\n11. \tОни ревнуют и завидуют \r\n12. \tВремя \\r\n13. \tГимн заходящего солнца" },
                new MainAppShop.Domain.Entities.User.Product { Id = 9, Name = "Здравствуй, Это Я…", Artist = "Руки Вверх", ImageUrl = "/Content/Images/album9.png", Price = 30, Description = "«Здравствуй, это я» — пятый альбом российской группы «Руки Вверх!», выпущенный в 2000 году. Альбом по традиции вышел весной. Альбом многими критиками признан успешным и был тепло встречен поклонниками группы и теми, кто первый раз познакомился с её творчеством. Это первый альбом, который вышел без первого продюсера группы Андрея Маликова, с которым Сергей и Алексей расстались из-за финансовых разногласий. В момент выпуска альбома был создан лейбл B-Funky (Пляшущие человечки), который занимался поиском и раскруткой молодых талантов в направлении танцевальной музыки. Список композиций: 1. Ай-яй-яй\r\n2. Он не любит тебя\r\n3. Киска-любовь\r\n4. Прости\r\n5. I like\r\n6. Не зови меня красивою\r\n7. Парень\r\n8. Баю-бай\r\n9. Так тебе и надо\r\n10. Песенка 3\r\n11. Здравствуй, это я\r\n12. Алёшка" },
                new MainAppShop.Domain.Entities.User.Product { Id = 10, Name = "Босоногий мальчик", Artist = "Леонид Агутин", ImageUrl = "/Content/Images/album10.png", Price = 25, Description = "Босоногий мальчик» — первый сольный студийный альбом российского музыканта Леонида Агутина, вышедший в 1994 году. Список композиций: 1.\t«Босоногий мальчик»\r\n2.\t«За счастьем» \r\n3.\t«Разговор о дожде» \r\n4.\t«Девочка» \r\n5.\t«Никому не сестра» \r\n6.\t«Голос высокой травы» \r\n7.\t«Следом за весной» \r\n8.\t«Хоп Хэй Лала Лэй» \r\n9.\t«Вспомни о нём» \r\n10.\t«Парень чернокожий» \r\n11.\t«Всё только для тебя» \r\n12.\t«Кого не стоило бы ждать»" },
                new MainAppShop.Domain.Entities.User.Product { Id = 11, Name = "Небы", Artist = "Ёлка", ImageUrl = "/Content/Images/album11.png", Price = 25, Description = "«#Небы» — пятый студийный альбом украинско-российской певицы Ёлки. Был записан в течение нескольких лет. Выпущен на лейбле Velvet Music 17 февраля 2015 года. На физических носителях диск вышел 25 февраля 2015 года. В ноябре состоялась презентация альбома в московском концертном зале Crocus City Hall. Вошёл в список 20 самых продаваемых альбомов 2015 года. Треки на альбоме выдержаны в жанре поп. Продюсерами альбома выступили Лев Трофимов, Алёна Михайлова и Лиана Меладзе. Диск содержит 14 композиций, написанных различными авторами. Список композиций: 1. \t«Нарисуй мне небо»\r\n2. \t«Хочу»\r\n3. \t«Тело офигело»\r\n4. \t«Одна»\r\n5. \t«Всё зависит от нас»\r\n6. \t«Ты знаешь» (совместно с Burito)\r\n7. \t«Лети, Лиза»\r\n8. \t«Прохожий»\r\n9. \t«Этажи»\r\n10. \t«Прекрасна»\r\n11. \t«Домой»\r\n12. \t«Пара»\r\n13. \t«Моревнутри»\r\n14. \t«Новое небо»" },
                new MainAppShop.Domain.Entities.User.Product { Id = 12, Name = "Солнце", Artist = "Ани Лорак", ImageUrl = "/Content/Images/album12.png", Price = 25, Description = "Солнце — девятый студийный и четвёртый русскоязычный альбом украинской певицы Ани Лорак, записанный в 2007 — 2009 годах, на студии VOX Recording Studios в Афинах. Релиз состоялся 15 июля 2009 года на лейбле United Music Group. Автором музыки и продюсером альбома выступил греческий композитор музыкальный продюсер Димитрис Контопулос. Авторы стихов - Карен Кавалерян, Андрей Морсин. Исполнительным продюсером был Филипп Киркоров. Альбом получил платиновый статус. Список композиций: 1. \t«Shady Lady»\r\n2. \t«Птица»\r\n3. \t«А дальше...»\r\n4. \t«Танцы»\r\n5. \t«Небеса-ладони»\r\n6. \t«Солнце»\r\n7. \t«Мечта о тебе»\r\n8. \t«Дальние страны»\r\n9. \t«Идеальный мир»\r\n10. \t«Пламя»\r\n11. \t«Нелюбовь»\r\n12. \t«Крылья чудес»\r\n13. \t«С неба в небо»\r\n14. \t«I'm Alive»" }
            };

                db.Products.AddRange(newProducts);
                db.SaveChanges();
            }

            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery) || p.Artist.Contains(searchQuery));
            }

            return View(products.ToList());
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
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        public ActionResult ProductsForAdmin()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        [RoleAuthorize("User", "Admin")]
        public ActionResult UserDashboard()
        {
            ViewBag.Login = User.Identity.Name;
            return View("~/Views/Home/UserDashboard.cshtml");
        }
    }
}