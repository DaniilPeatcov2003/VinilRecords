using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MainAppShop.BusinessLogic.Core.Admin
{
    public class AdminApi
    {
        public List<Product> GetAllProducts(string searchTerm)
        {
            using (var db = new UserContext())
            {
                var query = db.Products.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(p => p.Name.Contains(searchTerm) || p.Artist.Contains(searchTerm));
                }
                return query.ToList();
            }
        }

        public Product GetProductById(int id)
        {
            using (var db = new UserContext())
            {
                return db.Products.FirstOrDefault(p=>p.Id == id);
            }
        }

        public void CreateProduct(Product product)
        {
            using (var db = new UserContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var db = new UserContext())
            {
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var db = new UserContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
        }

        public List<UDbTable> GetAllUsers()
        {
            using (var db = new UserContext())
            {
                return db.Users.ToList();
            }
        }

        public void DeleteUser(int id)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
        }
    }
}