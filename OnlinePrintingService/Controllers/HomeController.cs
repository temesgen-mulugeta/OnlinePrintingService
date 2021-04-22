using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pricing()
        {
            return View();
        }
    }
}