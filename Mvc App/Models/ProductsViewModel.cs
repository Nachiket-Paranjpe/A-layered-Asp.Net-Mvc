using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_App.Models
{
    public class ProductsViewModel
    {
        public int ProductID { get; set; }
        [Required(AllowEmptyStrings = false , ErrorMessage = "Product Name is Required")]
        public String ProductName { get; set; }

        [Required(AllowEmptyStrings =false)]
        [Range(0,999.99)]
        public double UnitPrice { get; set; }
        [Range(0,10000)]
        public int UnitsInStock { get; set; }
        public Byte[] RowVersion { get; set; }
        public bool ConcurrencyResolved { get; set; } = false;
        public Dictionary<string, string> concurrencyMessages { get; set; }
    }
}