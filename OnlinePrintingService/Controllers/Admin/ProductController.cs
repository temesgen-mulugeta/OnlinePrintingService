
using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public static IEnumerable<Product> getAllProducts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var responseTask = client.GetAsync("Products");
                responseTask.Wait();

                var result = responseTask.Result;
                IEnumerable<Product> products;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();
                    products = readTask.Result;
                }
                else
                {
                    Debug.Print(result.ReasonPhrase);
                    products = Enumerable.Empty<Product>();
                }
                return products;
            }
        }


        [HttpGet]
        public ActionResult Products()
        {
            var products = getAllProducts();
            if (products.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(products);

        }

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


        public ActionResult deleteProduct(long ProductID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/Products");
                var deleteTask = client.DeleteAsync("Products/" + ProductID.ToString());
                deleteTask.Wait();
                return RedirectToAction("Products", "Product");
            }
        }
    }
   
}