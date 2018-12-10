using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossCuttingLayer.ModelEntities;

namespace BusinessAccessLayer.Business_Object_Interfaces
{
  public  interface IOrdersBO
    {
        void AddOrder(OrdersDTO order);
        IEnumerable<OrdersDTO> GetAllOrders();
        IEnumerable<OrderDetailsDTO> GetOrderDetailsBy(int orderID);
        ProductsDTO GetProductByOrderDetail(int id);
        void UpdateOrder(OrdersDTO updOrder);
        void DeleteOrder(OrdersDTO order);
        void DeleteOrder(int OrderID);
        Dictionary<string, string> SaveChangesFor(Operations op, OrdersDTO order, int? id);
        Task<Dictionary<string, string>> ValidateEntity();
    }
}
