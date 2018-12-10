using AutoMapper;
using CrossCuttingLayer.ModelEntities;
using Mvc_App.Helper;
using Mvc_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc_App.Controllers
{
    public class ProductsController : Controller
    {
        private IEnumerable<ProductsViewModel> _productvm = null;
       
        //The URL of the WEB API Service
        private string ServiceUrl { get; set; }
        private HttpClient GetSingletonInstance { get { return Servicesingleton.Instance; } }

        // GET: Products         
        public async Task<ActionResult> Index()
        {
           try
            {
                IEnumerable<ProductsDTO> getProducts = null;
             //   IDatabase _cache = Multiplexer.ConnectionInstance();
                //if (!_cache.StringGet(string.Format("GetAll:{0}", "Product")).HasValue)
                //{
                    ServiceUrl = GetSingletonInstance.BaseAddress + "api/HomeProducts";
                    HttpResponseMessage responsemessage = await GetSingletonInstance.GetAsync(ServiceUrl);

                    if (responsemessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        getProducts = JsonConvert.DeserializeObject<IEnumerable<ProductsDTO>>(await responsemessage.Content.ReadAsStringAsync());
                        _productvm = Mapper.Map<IEnumerable<ProductsDTO>, IEnumerable<ProductsViewModel>>(getProducts);
                    }
                 //   _cache.StringSet(string.Format("GetAll:{0}", "Product"), JsonConvert.SerializeObject(_productvm, formatting: Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.All }));
              //  }
                //else
                //{
                //    _productvm = JsonConvert.DeserializeObject<IEnumerable<ProductsViewModel>>(_cache.StringGet(string.Format("GetAll:{0}", "Product")));
                //}

                if (_productvm == null)
                    return HttpNotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }            

            return View(_productvm);
        }

        // GET: Products/Details/5        
        public async Task<ActionResult> Details(int id)
        {
            ProductsViewModel singleproduct = null;
            ServiceUrl = GetSingletonInstance.BaseAddress + "api/HomeProducts/" + id;
            HttpResponseMessage responsemessage = await GetSingletonInstance.GetAsync(ServiceUrl);
            try
            {
                responsemessage.EnsureSuccessStatusCode();

                if (responsemessage.IsSuccessStatusCode)
                {
                    singleproduct = JsonConvert.DeserializeObject<ProductsViewModel>(await responsemessage.Content.ReadAsStringAsync());               // responsemessage.Content.ReadAsAsync<ProductsDTO>(Formatters).Result;                    
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            if (singleproduct == null)
                return HttpNotFound();

            return View(singleproduct);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductsViewModel productvm)
        {
            try
            {
                ServiceUrl = GetSingletonInstance.BaseAddress + "api/products";
                HttpResponseMessage response = await GetSingletonInstance.PostAsJsonAsync(ServiceUrl, productvm);               // await GetSingletonInstance.PostAsync(ServiceUrl, productvm, Formatters.ToArray()[0]);
                response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5       
        public async Task<ActionResult> Edit(int id)
        {
            ServiceUrl = GetSingletonInstance.BaseAddress + "api/HomeProducts/" + id;
            ProductsViewModel singleproduct = null;
            HttpResponseMessage responsemessage = await GetSingletonInstance.GetAsync(ServiceUrl);
            try
            {
                if (responsemessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseData = responsemessage.Content.ReadAsStringAsync().Result;
                    singleproduct = Mapper.Map<ProductsViewModel>(JsonConvert.DeserializeObject<ProductsDTO>(responseData));
                }
            }
            catch (System.Exception ex)
            {
                return View("Error");
            }
            if (singleproduct == null)
                return HttpNotFound();

            return View(singleproduct);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProductsViewModel products)
        {
            try
            {
                ServiceUrl = GetSingletonInstance.BaseAddress + "api/UpdateProducts/" + id;
                ProductsViewModel prdvm = new ProductsViewModel();
                try
                {
                    if (TempData["concurrencyResolved"] != null)
                        products.ConcurrencyResolved = (bool)TempData["concurrencyResolved"];

                    HttpResponseMessage responsemessage = await GetSingletonInstance.PostAsJsonAsync(ServiceUrl, JsonConvert.SerializeObject(products));
                    responsemessage.EnsureSuccessStatusCode();

                    if (responsemessage.IsSuccessStatusCode)
                    {
                        var responseData = responsemessage.Content.ReadAsStringAsync().Result;

                        if (responseData != "null")
                        {
                            prdvm.concurrencyMessages = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseData);
                            TempData["concurrencyResolved"] = true;
                            AddMessagesToModelState(prdvm.concurrencyMessages);
                            return View(products);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    return View("Error", ex.InnerException.Message);
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ProductsViewModel productvm = new ProductsViewModel();
            try
            {
                ServiceUrl = GetSingletonInstance.BaseAddress + "api/Products/" + id;
                HttpResponseMessage responsemessage = await GetSingletonInstance.GetAsync(ServiceUrl);

                if (responsemessage.IsSuccessStatusCode)
                {
                    var responseData = JsonConvert.DeserializeObject<ProductsViewModel>(await responsemessage.Content.ReadAsStringAsync()); 

                    if (responseData !=null)
                    {
                        productvm = responseData;
                    }
                }
            }

            catch (System.Exception ex)
            {
                throw ex;
            }
            if (productvm == null)
                return View("Error");

            return View(productvm);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, string productName)
        {
            ProductsViewModel productvm = new ProductsViewModel();

            try
            {
                //if (TempData["concurrencyResolved"] != null)
                //    productvm.ConcurrencyResolved = (bool)TempData["concurrencyResolved"];

                ServiceUrl = GetSingletonInstance.BaseAddress + "api/Products/" + id;
                HttpResponseMessage responsemessage = await GetSingletonInstance.DeleteAsync(ServiceUrl);

                if (responsemessage.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    var responseData = responsemessage.Content.ReadAsStringAsync();
                    //productvm.concurrencyMessages = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseData.Result);
                    //TempData["concurrencyResolved"] = true;

                    ModelState.AddModelError("", "Record Deleted from database by another user");
                    return View();
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }
        private void AddMessagesToModelState(Dictionary<string, string> Messages)
        {
            ModelState.Clear();

            foreach (KeyValuePair<string, string> message in Messages)
            {
                ModelState.AddModelError(message.Key, string.Format("{0} contains a mismatch value of {1} with database value", message.Key, message.Value));
                ModelState.AddModelError("", "System found a concurrency conflict for the below item(s).\n Please review and submit the changes accordingly...");
            }
        }
    }
}

