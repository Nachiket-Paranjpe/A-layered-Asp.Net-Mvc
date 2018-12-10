using DataAccessLayer.Interfaces.BaseEntities;
using System;

namespace DataAccessLayer.Entities
{
    public partial class OrderDetail : IBaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public double UnitPrice { get; set; }
        public Int32 Quantity { get; set; }
        public double Discount { get; set; }     
        public byte[] RowVersion { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
