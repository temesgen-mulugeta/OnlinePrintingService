using OnlinePrintingService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pricing()
        {
            using (var context = new dbOPScontext())
            {
                List<Product> products = context.Product.ToList();
                return View(products);
            }
        }
    }
}