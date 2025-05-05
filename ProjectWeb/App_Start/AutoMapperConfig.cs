using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainAppShop.BusinessLogic.Core.User;
using ProjectWeb.Models;

namespace ProjectWeb.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserLogin, ULoginData>();
            });

            return config.CreateMapper();
        }
    }
}