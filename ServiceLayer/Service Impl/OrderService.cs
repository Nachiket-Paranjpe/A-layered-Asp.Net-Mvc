using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessAccessLayer.Business_Object_Interfaces;
using CrossCuttingLayer.ModelEntities;
using CrossCuttingLayer.Service_Interfaces;
using DataAccessLayer;

namespace ServiceLayer.Service_Impl
{
    public class OrderService : IOrderService
    {
        public IOrdersBO orderBO { get; set; }
        public OrderService(IOrdersBO orderBO)
        {
            this.orderBO = orderBO;
        }
        public OrderService()
        {}
        public void AddOrder(OrdersDTO order)
        {
            orderBO.AddOrder(order);  
        }

        public void DeleteOrder(OrdersDTO order)
        {
            orderBO.DeleteOrder(order);
        }

        public void DeleteOrder(int orderID)
        {
            orderBO.DeleteOrder(orderID);
        }
        public IEnumerable<OrdersDTO> GetAllOrders()
        {
            return orderBO.GetAllOrders();
        }
        public IEnumerable<OrderDetailsDTO> GetOrderDetailBy(int orderID)
        {
            return orderBO.GetOrderDetailsBy(orderID);
        }
        public ProductsDTO GetProductByOrderDetail(int id)
        {
            throw new System.NotImplementedException();
        }
        public Dictionary<string, string> SaveChangesFor(Operations op, OrdersDTO order, int? id)
        {
           return orderBO.SaveChangesFor(Operations.Update, order, null);
        }
        public Task<Dictionary<string, string>> ValidateEntity()
        {
            throw new System.NotImplementedException();
        }       
    }
}
