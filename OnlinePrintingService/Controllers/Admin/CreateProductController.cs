using OnlinePrintingService.ViewModel;
using OnlinePrintingServiceAPI.Models;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers.Admin
{
    public class CreateProductController : Controller
    {
        // GET: CreateProduct
        public ActionResult CreateProduct()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {
            using (var context = new dbOPScontext())
            {
                var product = new Product
                {
                    ProductName = productViewModel.ProductName,
                
                    Price = productViewModel.Price
                };

                context.Product.Add(product);
                context.SaveChanges();
                return RedirectToAction("Products", "Product");
            }
        }


    }
}