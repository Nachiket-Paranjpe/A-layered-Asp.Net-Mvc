using BusinessAccessLayer.Business_Object_Interfaces;
using CrossCuttingLayer.ModelEntities;
using CrossCuttingLayer.Service_Interfaces;
using CrossCuttingLayer.Helper.Caching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Service_Impl
{
    class ProductService : IProductService
    {
        public IProductsBO ProductBO { get; set; }
        public ProductService(IProductsBO productBO)
        {
            this.ProductBO = productBO;
        }
        public ProductService()
        {
        }

        public void AddProduct(ProductsDTO product)
        {
            ProductBO.AddProduct(product);
        }
        public void DeleteProduct(ProductsDTO product)
        {
           ProductBO.DeleteProduct(product);
        }
        public void DeleteProduct(int productID)
        {
           ProductBO.DeleteProduct(productID);
        }
       // [RedisCache]
        public IEnumerable<ProductsDTO> GetAllProducts()
        {
            return ProductBO.GetAllProducts();
        }
        public IEnumerable<ProductsDTO> GetAllProducts(int Unitsleft)
        {
            return ProductBO.GetAllProducts(Unitsleft);
        }

       // [RedisCache(CacheExpiration = 5)]
        public ProductsDTO GetProductBy(int id)
        {
            return ProductBO.GetProductBy(id);
        }

        [InvalidateRedisCache(CachedMethod = "GetProductBy")]
        private void UpdateProduct(ProductsDTO updproduct)
        {
           ProductBO.UpdateProduct(updproduct);            
        }
        public Task<Dictionary<string, string>> ValidateEntity()
        {
            throw new NotImplementedException();
        }
        public Dictionary<string, string> SaveChangesFor(Operations op, ProductsDTO product, int? id)
        {
            var result = ProductBO.SaveChangesFor(op, product, id);
            return result;
        }      
    }
}