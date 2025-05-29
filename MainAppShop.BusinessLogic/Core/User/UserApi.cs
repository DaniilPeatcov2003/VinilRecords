using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.Domain;
using MainAppShop.Domain.Entities.User;
using MainAppShop.Domain.User.Auth;
using MainAppShop.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using MainAppShop.BusinessLogic.DBModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Collections.Specialized.BitVector32;

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

        public bool AuthentificateUserAction(UDbTable userFromPL)
        {
            if (string.IsNullOrEmpty(userFromPL.Email)) return false;

            using (UserContext db = new UserContext())
            {
                string hashedPasswordFromInput = HashPassword(userFromPL.Password);
                var user = db.Users.FirstOrDefault(u => u.Email == userFromPL.Email && u.Password == hashedPasswordFromInput);
                if (user != null)
                {
                    user.LastLogin = DateTime.Now;
                    user.LasIp = "0.0.0.0";
                    db.Users.AddOrUpdate(user);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        internal HttpCookie Cookie(string mail)
        {
            var httpCookie = new HttpCookie("TWEB-D")
            {
                Value = CookieGenerator.Create(mail)
            };
            
            using (var db = new SessionContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(mail))
                {
                    var current = db.Sessions.FirstOrDefault(s => s.Email == mail);

                    if (current == null)
                    {
                        current = new Session
                        {
                            Email = mail,
                            CookieString = httpCookie.Value,
                            ExpireTime = DateTime.Now.AddDays(1)
                        };
                    }
                    else
                    {
                        current.CookieString = httpCookie.Value;
                        current.ExpireTime = DateTime.Now.AddDays(1);
                    }

                    db.Sessions.AddOrUpdate(current);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid email");
                }
            }

            return httpCookie;
        }

        internal int GetUserIdBySessionKeyAction(string sessionKey)
        {
            return 1;
        }

        internal ReceiptToPay GetReceiptToPayByUserIdAction(int uId)
        {
            return new ReceiptToPay();
        }

        public bool RegisterUserAction(UDbTable data)
        {
            using (var db = new UserContext())
            {
                // Проверка: есть ли уже пользователь с таким email
                var existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                if (existingUser != null)
                {
                    return false;
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

                return true;
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

        internal List<Product> GetAllAction(string search = null)
        {
            using (var db = new UserContext()) 
            {
                return string.IsNullOrWhiteSpace(search) || string.IsNullOrEmpty(search) ? db.Products.ToList() : db.Products.Where(p=>p.Name.Contains(search) || p.Artist.Contains(search)).ToList();
            }
        }

        internal Product GetByIdAction(int id)
        {
            using (var db = new UserContext())
            {
                return db.Products.FirstOrDefault(p => p.Id == id);
            }
        }
        internal bool SignOutAction(string cookie)
        {
            using (var db = new SessionContext())
            {
                var session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie);
                if (session == null) return false;
                db.Sessions.Remove(session);
                db.SaveChanges();
                return true;
            }
        }
        internal UserMinimal UserCookie(string cookie)
        {
            Session session;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie);
            }


            if (session == null) return null;

            if (session.ExpireTime < DateTime.Now)
            {
                SignOutAction(cookie);
                return null;
            }

            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Email == session.Email);
            }

            if (user == null) return null;


            var config = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<UDbTable, UserMinimal>(); });
            var mapper = config.CreateMapper();


            return mapper.Map<UserMinimal>(user);
        }
    }
    }