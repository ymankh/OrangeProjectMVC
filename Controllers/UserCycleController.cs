using OrangeProjectMVC.Models;
using System.Linq;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class UserCycleController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: UserCycle

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult contact()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult about()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult advertismentst()
        {
            ViewBag.Message = "Your application description page.";
            var ads = db.Ads.Where(ad => ad.status == "Active").ToList();
            return View(ads);
        }
    }
}
