
using OnlinePrintingServiceAPI.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Products()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/api/");
                var responseTask = client.GetAsync("Products");
                responseTask.Wait();

                var result = responseTask.Result;
                IQueryable<Product> products;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IQueryable<Product>>();
                    readTask.Wait();
                    products = readTask.Result;
                }
                else
                {
                    Debug.Print(result.ReasonPhrase);
                    products = Enumerable.Empty<Product>().AsQueryable();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(products);
            }
        }


        public ActionResult removeProduct(long ProductID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/api/Products");
                var deleteTask = client.DeleteAsync("Products/" + ProductID.ToString());
                deleteTask.Wait();
                return RedirectToAction("Products", "Product");
            }
        }
    }
   
}