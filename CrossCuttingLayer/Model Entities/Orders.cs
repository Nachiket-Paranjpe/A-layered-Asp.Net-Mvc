using System;
using System.Collections.Generic;

namespace CrossCuttingLayer.ModelEntities
{
    public class OrdersDTO
    {
        public OrdersDTO()
        {
            this.OrderDetails = new List<OrderDetailsDTO>();
        }
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }        
        public Byte[] RowVersion { get; set; }
        public virtual ICollection<OrderDetailsDTO> OrderDetails{ get; set; }
       
    }
}
