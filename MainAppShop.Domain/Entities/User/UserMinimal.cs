using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainAppShop.Domain.Entities.User
{
    public class UserMinimal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public URole Level { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
