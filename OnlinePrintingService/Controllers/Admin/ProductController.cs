
using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Products()
        {
            using (var context = new dbOPScontext())
            {
                List<Product> products = context.Product.ToList();
                return View(products);
            }
        }


       // [HttpPost]
        public ActionResult removeProduct(long ProductID)
        {
            using (var context = new dbOPScontext())
            {
                var product = new Product
                {
                    ProductID = ProductID,    
                };

                context.Product.Attach(product);
                context.Product.Remove(product);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }
   
}