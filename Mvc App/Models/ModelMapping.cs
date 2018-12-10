using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CrossCuttingLayer.ModelEntities;

namespace Mvc_App.Models
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<ProductsViewModel, ProductsDTO>().ReverseMap();           
        }
    }
}