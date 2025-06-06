﻿using MainAppShop.BusinessLogic.Core.User;
using MainAppShop.BusinessLogic.DBModel.Seed;
using MainAppShop.BusinessLogic.Interface;
using MainAppShop.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.ILogic
{
    public class ProductBl : UserApi, IProduct
    {
        public bool IsProductValid(int id)
        {
            return IsProductValidAction(id);
        }

        public List<Product> GetAll(string search = null)
        { 
            return GetAllAction(search);
        }

        public Product GetById(int id)
        {
            return GetByIdAction(id);
        }
    }
}
