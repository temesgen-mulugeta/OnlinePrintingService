using OnlinePrintingService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace OnlinePrintingService.REST
{
    public static class OrderREST
    {
        [HttpGet]
        public static IEnumerable<Order> GetAllOrder()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var responseTask = client.GetAsync("Orders");
                responseTask.Wait();

                var result = responseTask.Result;
                IEnumerable<Order> orders;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Order>>();
                    readTask.Wait();
                    orders = readTask.Result;
                }
                else
                {
                    Debug.Print(result.ReasonPhrase);
                    orders = Enumerable.Empty<Order>();
                }
                return orders;
            }
        }

        [HttpGet]
        public static Order GetOrder(long OrderID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var responseTask = client.GetAsync("Orders" + OrderID.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                Order order = null;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Order>();
                    readTask.Wait();
                    order = readTask.Result;
                }
                else
                {
                    Debug.Print(result.ReasonPhrase);
                    
                }
                return order;
            }
        }

        [HttpPost]
        public static HttpResponseMessage Post(Order order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var postTask = client.PostAsJsonAsync<Order>("Orders", order);
                postTask.Wait();
                return postTask.Result;

            }
        }


        public static HttpResponseMessage Delete(long OrderID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var deleteTask = client.DeleteAsync("Orders/" + OrderID.ToString());
                deleteTask.Wait();
                return deleteTask.Result;
            }
        }
    }
}
}