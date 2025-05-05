using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.Domain;
using MainAppShop.Domain.Entities.User;
using MainAppShop.Domain.User.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Core.User
{
    public class UserApi
    {
        public UserApi() { }

        public bool IsSessionValidAction(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return true;
        }

        public bool IsProductValidAction(int id)
        {
            return true;
        }

        public string AuthentificateUserAction(UserAuthAction auth)
        {
            return "";
        }

        internal int GetUserIdBySessionKeyAction(string sessionKey)
        {
            return 1;
        }

        internal ReceiptToPay GetReceiptToPayByUserIdAction(int uId)
        {
            return new ReceiptToPay();
        }

        public string RegisterUserAction(ULoginData data)
        {
            using (var db = new UserContext())
            {
                // Проверка: есть ли уже пользователь с таким email
                var existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                if (existingUser != null)
                {
                    return "Этот email уже зарегистрирован!";
                }

                // Новый пользователь
                var user = new UDbTable
                {
                    Name = data.Email,  // Или data.Name, если хочешь отдельное имя
                    Password = HashPassword(data.Password), // !!! тут будет зашифрованный пароль
                    Email = data.Email,
                    LastLogin = DateTime.Now,
                    LasIp = "0.0.0.0", // Можно потом получить IP клиента
                    Level = URole.User // По умолчанию обычный пользователь
                };

                db.Users.Add(user);
                db.SaveChanges();

                return "Регистрация прошла успешно!";
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}