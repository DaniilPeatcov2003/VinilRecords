﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAppShop.BusinessLogic.Interface
{
    public interface IProduct
    {
        bool IsProductValid(int id);
    }
}
