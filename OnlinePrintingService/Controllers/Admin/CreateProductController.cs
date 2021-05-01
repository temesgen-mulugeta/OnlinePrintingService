using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers.Admin
{
    public class CreateProductController : Controller
    {
        public ActionResult CreateProduct()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {
            
                var product = new Product
                {
                    ProductName = productViewModel.ProductName,
                    Price = productViewModel.Price
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44398/api/");
                    var postTask = client.PostAsJsonAsync<Product>("Products", product);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Products", "Product");
                    }
                    Debug.Print(result.ReasonPhrase);
                    ModelState.AddModelError(string.Empty, "Sorry, something went wrong.");
                }
                return View();
        }
    }
}