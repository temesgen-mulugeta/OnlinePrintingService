
using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new dbOPScontext())
            {
                List<Product> products = context.Product.ToList();
                return View(products);
            }
        }

        [HttpPost]
        public ActionResult createProduct(ProductViewModel productViewModel)
        {
            using (var context = new dbOPScontext())
            {
                var product = new Product
                {
                    ProductName = productViewModel.ProductName,
                    ProductSize = productViewModel.ProductSize,
                    Price = productViewModel.Price
                };

                context.Product.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult removeProduct(long ProductID)
        {
            using (var context = new dbOPScontext())
            {
                var product = new Product
                {
                    ProductID = ProductID,    
                };

                context.Product.Remove(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }
   
}