using AutoMapper;
using BusinessAccessLayer.Business_Object_Interfaces;
using CrossCuttingLayer.ModelEntities;
using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Interface_Implementation
{
    public class ProductsBO : IProductsBO
    {
        private IUnitofwork UoW { get; set; }
        private bool saveChanges { get; set; } = true;
        public IMapper Mapper { get; set; }
        public ProductsBO(IUnitofwork uow )
        {
            this.UoW = uow;            
        }
        public ProductsBO()
        {

        }
        public void AddProduct(ProductsDTO product)
        {
            UoW.Repository<Product>().Add(Mapper.Map<Product>(product));
            UoW.Save(saveChanges);
        }       
        public IEnumerable<ProductsDTO> GetAllProducts()
        {
            return Mapper.Map <IEnumerable<ProductsDTO>>(UoW.Repository<Product>().Query().GetAll());
        }
        public IEnumerable<ProductsDTO> GetAllProducts(int Unitsleft)
        {
            return Mapper.Map<IEnumerable<ProductsDTO>>(UoW.Repository<Product>().Find(prd => prd.UnitsInStock == Unitsleft));
        }        
        public ProductsDTO GetProductBy(int id)
        {
            return Mapper.Map<ProductsDTO>(UoW.Repository<Product>().Get(id));
        }
        public void DeleteProduct(ProductsDTO product)
        {
            UoW.Repository<Product>().Remove(Mapper.Map<ProductsDTO, Product>(product));
        }
        public void DeleteProduct(int productID)
        {
            UoW.Repository<Product>().Remove(productID);
            UoW.Save(saveChanges);
        }
        public void UpdateProduct(ProductsDTO updproduct)
        {
            UoW.Repository<Product>().Update(Mapper.Map<Product>(updproduct));          
        }      
        public Task<Dictionary<string, string>> ValidateEntity()
        {
            throw new NotImplementedException();
        } 
        public Dictionary<string, string> SaveChangesFor(Operations op, ProductsDTO product, int? id)
        {
            Task<Dictionary<string, string>> Recordsmismatch = null;
            switch (op)
            {
                case Operations.Update:
                    UpdateProduct(product);
                    Recordsmismatch = Task.Run(async () => await UoW.CheckandResolveConcurrencyIssuesAsync(product.ConcurrencyResolved));
                    saveChanges = Recordsmismatch.Result == null ? true : false;
                    break;
               
                default:
                    break;
            }

            if (saveChanges)
                UoW.Save(saveChanges);
            return Recordsmismatch.Result;
        }
    }
}
