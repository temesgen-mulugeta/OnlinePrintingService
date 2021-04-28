
using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

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