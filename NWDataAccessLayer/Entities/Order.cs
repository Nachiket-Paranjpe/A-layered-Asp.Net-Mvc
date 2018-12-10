using DataAccessLayer.Interfaces.BaseEntities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Order : IBaseEntity
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public Byte[] RowVersion { get; set; }        
        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> Order_Details { get; set; }
    }
}
