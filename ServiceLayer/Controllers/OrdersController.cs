using CrossCuttingLayer.ModelEntities;
using CrossCuttingLayer.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ServiceLayer.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private IOrderService _orderService;
        private IOrderService OrderService { get => _orderService; set => _orderService = value; }

        public OrdersController(IOrderService orderservice) => OrderService = orderservice;

        // GET: api/Orders
        [Route("GetAllOrders")]
        public IHttpActionResult Get()
        {
            return Ok(OrderService.GetAllOrders());
        }
       
        // GET: api/Orders/5
        [HttpGet]
        [Route("GetOrderDetails/{orderid:int}")]
        [ResponseType(typeof(IEnumerable<OrderDetailsDTO>))]
        public IHttpActionResult GetOrderDetail(int orderid)
        {
            return Ok(OrderService.GetOrderDetailBy(orderid));
        }
        // POST: api/Orders
        [HttpPost]
        [Route("AddOrder")]
        public IHttpActionResult AddOrder(OrdersDTO newOrder)
        {
            OrderService.AddOrder(newOrder);
            return Ok();
        }

        // PUT: api/Orders/5
        [HttpPut]
        [Route("UpdateOrder")]
        public void UpdateOrder(int id, OrdersDTO order)
        {
            OrderService.SaveChangesFor(Operations.Update, order, id);
        }

        // DELETE: api/Orders/5
        [HttpDelete]
        [Route("DeleteOrder")]
        public void Delete(int id)
        {
            OrderService.DeleteOrder(id);
        }
    }
}
