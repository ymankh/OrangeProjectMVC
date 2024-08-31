using OrangeProjectMVC.Models;
using System;
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
            var dates = db.Dates.FirstOrDefault();

            var dateNow = DateTime.Now;
            var date = db.Dates.FirstOrDefault();
            if (dateNow < date.nomination_start_date)
            {
                ViewBag.Date = date.nomination_start_date;
                ViewBag.TimerTitle = "الوقت  حتى بداية فترة الترشح";
            }
            else if (dateNow < date.nomination_end_date)
            {
                ViewBag.Date = date.nomination_end_date;
                ViewBag.TimerTitle = "الوقت  حتى انتهاء فترة الترشح";
            }
            else if (dateNow < date.election_start_date)
            {
                ViewBag.Date = date.election_start_date;
                ViewBag.TimerTitle = "الوقت  حتى بداية فترة التصويت";
            }
            else if (dateNow < date.election_end_date)
            {
                ViewBag.Date = date.election_end_date;
                ViewBag.TimerTitle = "الوقت  حتى انتهاء فترة التصويت";
            }
            
             
            return View();
        }

        //public ActionResult contact()
        //{
        //    ViewBag.Message = "Your application description page.";
        //    return View();
        //}

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
