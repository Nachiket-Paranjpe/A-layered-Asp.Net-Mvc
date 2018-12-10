using System;
using CrossCuttingLayer.ModelEntities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCuttingLayer.Service_Interfaces
{
    public interface IOrderService
    {
        void AddOrder(OrdersDTO order);
        IEnumerable<OrdersDTO> GetAllOrders();
        IEnumerable<OrderDetailsDTO> GetOrderDetailBy(int orderID);
        ProductsDTO GetProductByOrderDetail(int id);
        void DeleteOrder(OrdersDTO order);
        void DeleteOrder(int orderID);
        Dictionary<string, string> SaveChangesFor(Operations op, OrdersDTO order, int? id);
        Task<Dictionary<string, string>> ValidateEntity();
    }
}
