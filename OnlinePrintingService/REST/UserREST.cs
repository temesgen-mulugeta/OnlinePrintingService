using OnlinePrintingService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace OnlinePrintingService.REST
{
    public static class UserREST
    {
        [HttpGet]
        public static IEnumerable<UserModel> GetAllUsers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44398/api/Account/");
                var responseTask = client.GetAsync("All");
                responseTask.Wait();

                var result = responseTask.Result;
                IEnumerable<UserModel> users;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserModel>>();
                    readTask.Wait();
                    users = readTask.Result;
                }
                else
                {
                    Debug.Print(result.ReasonPhrase);
                    users = Enumerable.Empty<UserModel>();
                }
                return users;
            }
        }

    }
}
