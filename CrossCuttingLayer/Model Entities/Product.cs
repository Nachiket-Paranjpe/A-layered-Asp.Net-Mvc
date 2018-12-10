using System;

namespace CrossCuttingLayer.ModelEntities
{
    public class ProductsDTO
    {
        public int ProductID { get; set; }
        public String ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public Byte[] RowVersion { get; set; }
        public bool ConcurrencyResolved { get; set; }
    }
}
