using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessAccessLayer.Business_Object_Interfaces;
using CrossCuttingLayer.ModelEntities;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace ServiceLayer
{
    public class OrdersBO : IOrdersBO
    {
        public IUnitofwork UoW { get; set; }
        private bool saveChanges { get; set; } = true;
        public IMapper Mapper { get; set; }
        public OrdersBO(IUnitofwork uow)
        {
            this.UoW = uow;
        }
        public void AddOrder(OrdersDTO order)
        {
            UoW.Repository<Order>().Add(Mapper.Map<Order>(order));
        }
        public void DeleteOrder(OrdersDTO order)
        {
            UoW.Repository<Order>().Remove(Mapper.Map<Order>(order));
        }
        public void DeleteOrder(int OrderID)
        {
            UoW.Repository<Order>().Remove(OrderID);
        }
        public IEnumerable<OrdersDTO> GetAllOrders()
        {
          return  Mapper.Map<IEnumerable<OrdersDTO>>(UoW.Repository<Order>().Query().GetAll());
        }
        public IEnumerable<OrderDetailsDTO> GetOrderDetailsBy(int orderID)
        {
          var order= UoW.Repository<Order>().Query().GetChildEntities(ord => ord.OrderID == orderID);
            return Mapper.Map<IEnumerable<OrderDetailsDTO>>(order.Order_Details);            
        }

        public ProductsDTO GetProductByOrderDetail(int id)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> SaveChangesFor(Operations op, OrdersDTO order, int? id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateOrder(OrdersDTO updOrder)
        {
            throw new System.NotImplementedException();
        }

        public Task<Dictionary<string, string>> ValidateEntity()
        {
            throw new System.NotImplementedException();
        }        
    }
}