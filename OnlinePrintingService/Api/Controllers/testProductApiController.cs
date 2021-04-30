using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlinePrintingServiceAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace OnlinePrintingService.API.Controllers
{
    public class ProductApiController : ApiController
    {
        [HttpGet, Route("api/products")]
        public List<Product> Get()
        {
            using (var context = new dbOPScontext())
            {
                List<Product> products = context.Product.ToList();
                return products;
            }
        }

        [HttpPost, Route("api/products/{productJson}")]
        public void AddProduct(JObject productJson)
        {
            using (var context = new dbOPScontext())
            {
                var product = JsonConvert.DeserializeObject<Product>(productJson.ToString());
                context.Product.Add(product);
                context.SaveChanges();
            }
        }

        [HttpDelete, Route("api/products/{productID}")]
        public void DeleteProduct(long productID)
        {
            using (var context = new dbOPScontext())
            {
                var product = new Product
                {
                    ProductID = productID,
                };

                context.Product.Attach(product);
                context.Product.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
