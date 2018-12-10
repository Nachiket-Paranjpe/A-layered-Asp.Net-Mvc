using CrossCuttingLayer.ModelEntities;
using Mvc_App.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc_App.Controllers
{
    public class OrdersController : Controller
    {
        private string ServiceUrl { get; set; }
        private readonly string _apiPrefix = "api/orders/";
        public HttpClient GetSingletonInstance { get { return Servicesingleton.Instance; } }

        // GET: Orders       
        public async Task<ActionResult> Index()
        {
            IEnumerable<OrdersDTO> orders = null;
            try
            {                
                ServiceUrl = GetSingletonInstance.BaseAddress + _apiPrefix + "GetAllOrders";

                HttpResponseMessage response = await GetSingletonInstance.GetAsync(ServiceUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    orders = JsonConvert.DeserializeObject<IEnumerable<OrdersDTO>>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(orders);
        }

        //GET: Orders/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }
        // GET: Orders/OrderDetails/5
        public async Task<ActionResult> OrderDetails(int id)
        {
            try
            {
                IEnumerable<OrderDetailsDTO> orderdetails = null;
                ServiceUrl = GetSingletonInstance.BaseAddress + _apiPrefix +"GetOrderDetails/" + id.ToString();

                HttpResponseMessage response = await GetSingletonInstance.GetAsync(ServiceUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    orderdetails = JsonConvert.DeserializeObject<IEnumerable<OrderDetailsDTO>>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OrdersDTO order)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
