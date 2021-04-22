using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlinePrintingService.Helper
{
    public class CookieData
    {
        public string userName;
        public string role;

        public CookieData(string userName, string role)
        {
            this.userName = userName;
            this.role = role;
        }
    }
}