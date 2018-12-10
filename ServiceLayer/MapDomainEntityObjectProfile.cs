using AutoMapper;
using CrossCuttingLayer.ModelEntities;
using DataAccessLayer.Entities;

namespace ServiceLayer
{
    internal class MapDomainEntityObjectProfile : Profile
    {
        public MapDomainEntityObjectProfile()
        {
            CreateMap<Product, ProductsDTO>().ReverseMap();
            CreateMap<Order, OrdersDTO>().ReverseMap();            
            CreateMap<OrderDetail, OrderDetailsDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}