using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlinePrintingService.ViewModel
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username can't be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }

       
        public LoginViewModel() { }
        public LoginViewModel(string usr,string pss) {

            
            UserName = usr;
            Password = pss;
            
            var task = Task.Run(async () => await check());
           
        }

        public async Task  check()
        {
            string user = UserName;
            string pass = Password;
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            nvc.Add(new KeyValuePair<string, string>("username", user));
            nvc.Add(new KeyValuePair<string, string>("password", pass));
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/token") { Content = new FormUrlEncodedContent(nvc) };
            var res = await client.SendAsync(req);
            UserInformation.response = res;
            UserInformation.message = res.ReasonPhrase;

        }
    }
}