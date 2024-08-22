using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OrangeProjectMVC.Models;

namespace OrangeProjectMVC.Controllers
{
    public class DatesController : Controller
    {
        private electionEntities db = new electionEntities();

        public ActionResult Edit()
        {

            var date = db.Dates.FirstOrDefault();
            if (date != null) return View(date);
            date = new Date();
            db.Dates.Add(date);
            db.SaveChanges();
            return View(date);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nomination_start_date,nomination_end_date,election_start_date,election_end_date,results_date")] Date date)
        {
            if (!ModelState.IsValid) return View(date);
            db.Entry(date).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
