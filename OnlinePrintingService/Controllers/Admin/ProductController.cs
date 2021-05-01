
using OnlinePrintingService.Models;
using OnlinePrintingService.REST;
using OnlinePrintingService.ViewModel;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class ProductController : Controller
    {

        [HttpGet]
        public ActionResult Products()
        {
            var products = ProductREST.GetAllProducts();
            if (products.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(products);

        }

        public ActionResult CreateProduct() => View();
        
        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {

            var product = new Product
            {
                ProductName = productViewModel.ProductName,
                Price = productViewModel.Price
            };

            var result = ProductREST.Post(product);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Products", "Product");
            }
            Debug.Print(result.ReasonPhrase);
            ModelState.AddModelError(string.Empty, "Sorry, something went wrong.");

            return View();
        }


        public ActionResult DeleteProduct(long ProductID)
        {
            ProductREST.Delete(ProductID);
            return RedirectToAction("Products", "Product");
        }
    }
   
}