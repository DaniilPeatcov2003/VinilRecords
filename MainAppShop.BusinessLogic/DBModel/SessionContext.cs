using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainAppShop.Domain.Entities.User;

namespace MainAppShop.BusinessLogic.DBModel
{
    public class SessionContext: DbContext
    {
        public SessionContext() : base("name=ProjectWeb")
        {
        }

        public virtual DbSet<Session> Sessions {  get; set; }
    }
}
