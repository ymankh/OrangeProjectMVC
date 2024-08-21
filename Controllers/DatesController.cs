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

        // GET: Dates
        //public ActionResult Index()
        //{
        //    return View(db.Dates.ToList());
        //}

        // GET: Dates/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Date date = db.Dates.Find(id);
        //    if (date == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(date);
        //}

        //// GET: Dates/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Dates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,nomination_start_date,nomination_end_date,election_start_date,election_end_date,results_date")] Date date)
        //{
        //    if (!ModelState.IsValid) return View(date);
        //    db.Dates.Add(date);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        // GET: Dates/Edit/5
        public ActionResult Edit()
        {

            var date = db.Dates.FirstOrDefault();
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        // POST: Dates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nomination_start_date,nomination_end_date,election_start_date,election_end_date,results_date")] Date date)
        {
            if (!ModelState.IsValid) return View(date);
            db.Entry(date).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // GET: Dates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var date = db.Dates.Find(id);
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        // POST: Dates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var date = db.Dates.Find(id);
            db.Dates.Remove(date);
            db.SaveChanges();
            return RedirectToAction("Index");
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
