using CrossCuttingLayer.ModelEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossCuttingLayer.Service_Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductsDTO product);
        IEnumerable<ProductsDTO> GetAllProducts();
        IEnumerable<ProductsDTO> GetAllProducts(int Unitsleft);
        ProductsDTO GetProductBy(int id);
        void DeleteProduct(ProductsDTO product);
        void DeleteProduct(int productID);       
        Dictionary<string, string> SaveChangesFor(Operations op, ProductsDTO product, int? id);
        Task<Dictionary<string, string>> ValidateEntity();
    }
}
