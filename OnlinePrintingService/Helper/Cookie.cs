using System;
using System.Web;

namespace OnlinePrintingService.Helper
{
    public class Cookie
    {
      

        public static void AddCookie(String userName, String role, HttpResponse response) {
            HttpCookie userData;
            userData = new HttpCookie("userData");
            userData["userName"] = $"{userName}";
            userData["role"] = $"{role}";
            userData.Expires = DateTime.Now.AddDays(30);
            response.Cookies.Add(userData);
        }
        public static void RemoveCookie(HttpResponse response) => response.Cookies["userData"].Expires = DateTime.Now.AddDays(-1);



        public static CookieData GetCookieData(HttpRequest request)
        {
            var userName = request.Cookies["userData"].Value.Substring(request.Cookies["userData"].Value.IndexOf('=') + 1, request.Cookies["userData"].Value.IndexOf('&') - 1);
            var role = request.Cookies["userData"].Value.Substring(request.Cookies["userData"].Value.IndexOf("e=") + 2);

            return new CookieData(userName, role);

        }

        public static bool isUserLoggedIn(HttpRequest request) =>
            request.Cookies["userData"] != null &&
            request.Cookies["userData"].Value != String.Empty;

    }
}