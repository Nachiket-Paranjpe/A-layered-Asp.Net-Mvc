using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using BusinessAccessLayer.Business_Object_Interfaces;
using BusinessAccessLayer.Interface_Implementation;
using CrossCuttingLayer.Service_Interfaces;
using DataAccessLayer;
using DataAccessLayer.Context;
using DataAccessLayer.Interfaces.Context;
using ServiceLayer.App_Start;
using ServiceLayer.Service_Impl;
using System;
using System.Reflection;
using System.Web.Http;

namespace ServiceLayer
{
    public class Autofac
    {
        public static void InjectDependencies(HttpConfiguration config)
        {
            // create you builder
            ContainerBuilder builder = new ContainerBuilder();              

            // Register your API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductsBO>().As<IProductsBO>().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterType<OrdersBO>().As<IOrdersBO>().PropertiesAutowired().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitofwork>().InstancePerLifetimeScope();
            builder.RegisterType<NwDBcontext>().As<IDbcontext>().InstancePerLifetimeScope();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();           

            builder.Register(c => AutoMapperProfiles.InitializeProfiles());
            builder.Register(c => c.Resolve<IConfigurationProvider>()
            .CreateMapper(c.Resolve)).As<IMapper>(); 

        // builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        // builder.RegisterAssemblyTypes(typeof(GenericRepository<Products>).Assembly).Where(t=> t.Name.EndsWith("Repository")).InstancePerLifetimeScope();

        IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            using (var scope = container.BeginLifetimeScope())
            {
                var Productservice = scope.Resolve<IProductService>();
                var Orderservice = scope.Resolve<IOrderService>();
              
                //var repo = scope.Resolve<IUnitofwork>();
            }
        }
    }
   
}