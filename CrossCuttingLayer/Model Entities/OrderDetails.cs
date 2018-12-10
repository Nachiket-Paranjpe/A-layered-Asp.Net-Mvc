using System;

namespace CrossCuttingLayer.ModelEntities
{
    public class OrderDetailsDTO
    {
       public int OrderID { get; set; }
       public int ProductID { get; set; }
       public double UnitPrice { get; set; }
       public Int32 Quantity { get; set; }
       public double Discount { get; set; }        
        public byte[] RowVersion { get; set; }
       public virtual OrdersDTO Orders { get; set; }
       public virtual ProductsDTO Products { get; set; }
    }
}
