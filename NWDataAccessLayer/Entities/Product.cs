using DataAccessLayer.Interfaces.BaseEntities;
using System;
namespace DataAccessLayer.Entities
{
    public partial class Product : IBaseEntity
    {
        public int ProductID { get; set; }
        public String ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }      
        public byte[] RowVersion { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual Category Category { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual Supplier Supplier { get; set; }
    }
}
