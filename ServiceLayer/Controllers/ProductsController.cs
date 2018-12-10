using CrossCuttingLayer.ModelEntities;
using CrossCuttingLayer.Service_Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace ServiceLayer.Controllers
{
    public class ProductsController : ApiController
    {
        public IProductService Service { get; set; } = null;

        public ProductsController(IProductService service)
        {
            this.Service = service;
        }

        [ResponseType(typeof(IEnumerable<ProductsDTO>))]

        [Route("api/HomeProducts")]
        public IHttpActionResult Get()
        {
            return Ok(Service.GetAllProducts());
        }

        // GET: api/Products/5        
        [ResponseType(typeof(ProductsDTO))]  
        [Route("api/HomeProducts/{id:int}")]
        public IHttpActionResult GetProductById(int id)
        {
            return Ok(Service.GetProductBy(id));
        }

        // POST: api/Products        
        public IHttpActionResult Post(ProductsDTO products)
        {
            Service.AddProduct(products);
            return Ok();
        }

        // PUT: api/Products/5
        [HttpPut]       
        [ResponseType(typeof(Dictionary<string, string>))]        
        [Route("~/api/UpdateProducts/{id:int}/{products}")]
        public IHttpActionResult Put(int id, ProductsDTO products)
        {
            return Ok(Service.SaveChangesFor(Operations.Update, products, null));
        }

        // DELETE: api/Products/5
        public IHttpActionResult Delete(int id)
        {
            // ProductsDTO product= _service.GetProductBy(id);
            Service.DeleteProduct(id);
            return Ok();
        }
    }
}
