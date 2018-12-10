using CrossCuttingLayer.ModelEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Business_Object_Interfaces
{
    public interface IProductsBO
    {
        void AddProduct(ProductsDTO product);        
        IEnumerable<ProductsDTO> GetAllProducts();
        IEnumerable<ProductsDTO> GetAllProducts(int Unitsleft);
        ProductsDTO GetProductBy(int id);
        void UpdateProduct(ProductsDTO updproduct);
        void DeleteProduct(ProductsDTO product);
        void DeleteProduct(int productID);
        Dictionary<string, string> SaveChangesFor(Operations op, ProductsDTO product, int? id);
        Task<Dictionary<string, string>> ValidateEntity();
    }
}
