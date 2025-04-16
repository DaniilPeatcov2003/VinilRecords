using System.Web.Mvc;
using MainAppShop.Domain.Entities.User;
using MainAppShop.BusinessLogic.DBModel.Seed;
using System;

public class UserController : Controller
{
    public ActionResult AddTestUser()
    {
        using (var db = new UserContext())
        {
            var user = new UDbTable
            {
                Name = "testuser",
                Password = "testpassword123",
                Email = "test@example.com",
                LastLogin = DateTime.Now,
                LasIp = "127.0.0.1",
                Level = URole.User
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        return Content("Пользователь добавлен!");
    }
}
