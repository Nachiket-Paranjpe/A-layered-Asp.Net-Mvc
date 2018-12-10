using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ServiceLayer.App_Start
{
    public class AutoMapperProfiles : Profile
    {
        public static IConfigurationProvider InitializeProfiles()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<MapDomainEntityObjectProfile>());

            return config;
        }
    }
}