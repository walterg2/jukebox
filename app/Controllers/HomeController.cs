using System.Web.Mvc;

namespace Jukebox.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Jukebox";

            return View();
        }
    }
}
