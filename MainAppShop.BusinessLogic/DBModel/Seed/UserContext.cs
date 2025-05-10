using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainAppShop.Domain.Entities.User;

namespace MainAppShop.BusinessLogic.DBModel.Seed
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("name=ProjectWeb")
        {
        }
        public virtual DbSet<UDbTable> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
