using OnlinePrintingService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace OnlinePrintingService.REST
{
    public static class ProductREST
    {

        [HttpGet]
        public static IEnumerable<Product> GetAllProducts()
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
        public static Product GetProduct(long ProductID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var responseTask = client.GetAsync("Products" + ProductID.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                Product product;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();
                    product = readTask.Result;
                }
                else
                {
                    Debug.Print(result.ReasonPhrase);
                    product = new Product { ProductID = 0, ProductName = "Not Available" };
                }
                return product;
            }
        }

        [HttpPost]
        public static HttpResponseMessage Post(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/Products");
                var postTask = client.PostAsJsonAsync<Product>("Products", product);
                postTask.Wait();
                return postTask.Result;
                
            }
        }


        public static HttpResponseMessage Delete(long ProductID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var deleteTask = client.DeleteAsync("Products/" + ProductID.ToString());
                deleteTask.Wait();
                return deleteTask.Result;
            }
        }
    }
}