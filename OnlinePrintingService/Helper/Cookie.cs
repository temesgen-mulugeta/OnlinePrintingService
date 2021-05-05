using System;
using System.Web;

namespace OnlinePrintingService.Helper
{
    public class Cookiez
    {
      

        public static void AddCookie(String userId, String role, HttpResponseBase response) {
            HttpCookie userData;
            userData = new HttpCookie("userData");
            userData["userId"] = $"{userId}";
            userData["role"] = $"{role}";
            userData.Expires = DateTime.Now.AddDays(30);
            response.Cookies.Add(userData);
        }
        public static void RemoveCookie(HttpResponse response) => response.Cookies["userData"].Expires = DateTime.Now.AddDays(-1);



        public static CookieData GetCookieData(HttpRequestBase request)
        {
            var userId = request.Cookies["userData"].Value.Substring(request.Cookies["userData"].Value.IndexOf('=') + 1, request.Cookies["userData"].Value.IndexOf('&') - 1);
            var role = request.Cookies["userData"].Value.Substring(request.Cookies["userData"].Value.IndexOf("role=") + 5);

            return new CookieData(userId, role);

        }

        public static bool isUserLoggedIn(HttpRequestBase request) =>
            request.Cookies["userData"] != null &&
            request.Cookies["userData"].Value != String.Empty;

    }
}