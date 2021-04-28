using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers.Admin
{
    public class CreateProductController : Controller
    {
        // GET: CreateProduct
        public ActionResult Index()
        {
            return View();
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


    }
}