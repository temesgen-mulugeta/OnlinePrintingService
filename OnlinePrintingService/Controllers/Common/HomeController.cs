using OnlinePrintingService.Models;
using OnlinePrintingService.REST;
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
            var products = ProductREST.GetAllProducts();
            if (products.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(products.ToList());
        }
    }
}
