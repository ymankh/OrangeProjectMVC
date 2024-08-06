using OrangeProjectMVC.Models;
using System.Linq;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class AdvController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: Ads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ad model)
        {
            if (ModelState.IsValid)
            {
                Ad ad = new Ad
                {
                    description = model.description,
                    img_url = model.img_url,
                    status = "Active"
                };

                db.Ads.Add(ad);
                db.SaveChanges();
                return RedirectToAction("Create"); // Redirect to an appropriate page
            }

            return View(model);
        }

        // GET: Ads
        public ActionResult Index()
        {
            var ads = db.Ads.ToList();
            return View(ads);
        }

        // GET: Ads/Edit/5
        public ActionResult Edit(int id)
        {
            var ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ad ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); // Redirect to a list or details view after editing
            }
            return View(ad);
        }

        // GET: Ads/Delete/5
        public ActionResult Delete(int id)
        {
            var ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ad = db.Ads.Find(id);
            db.Ads.Remove(ad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}