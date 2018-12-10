using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CrossCuttingLayer.ModelEntities;

namespace Mvc_App.Models
{
    public class OrdersViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Shipment Name is Required")]
        public string ShipName { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Shipment Address is Required")]
        public string ShipAddress { get; set; }
        [Range(0,999.99)]
        public double UnitPrice { get; set; }
        [Range(0,10000)]
        public Int32 Quantity { get; set; }
        public IEnumerable<OrderDetailsDTO> OrderList { get; set; }

    }
}