using OnlinePrintingService.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace OnlinePrintingService.ApiInfrastructure
{
    public interface ITokenContainer
    {
        object ApiToken { get; set; }
    }


    public class TokenContainer : ITokenContainer
    {
        private const string ApiTokenKey = "ApiToken";

        public object ApiToken
        {
            get { return Current.Session != null ? Current.Session[ApiTokenKey] : null; }
            set { if (Current.Session != null) Current.Session[ApiTokenKey] = value; }
        }

        private static HttpContextBase Current
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }
    }
}