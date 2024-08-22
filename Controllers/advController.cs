using OrangeProjectMVC.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OrangeProjectMVC.Controllers
{
    public class AdvController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: Ads/Create


        public ActionResult ChoosePackage()
        {
            return View();
        }
        public ActionResult Create(decimal price)
        {
            ViewBag.Price = price;
            return View();
        }



        // POST: Ads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ad model, HttpPostedFileBase ImageFile, decimal price)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Generate a unique filename and save the image
                    var fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    var extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + "_" + Guid.NewGuid().ToString() + extension;

                    // Define the path to save the uploaded file
                    var directoryPath = Server.MapPath("~/uploads/ads/");

                    // Ensure the directory exists
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Save the file
                    var path = Path.Combine(directoryPath, fileName);
                    ImageFile.SaveAs(path);

                    // Save the file path in the database
                    model.img_url = "~/uploads/ads/" + fileName;
                }

                model.status = "pending";
                db.Ads.Add(model);
                db.SaveChanges();
                return RedirectToAction("Checkout", "Payment", new { price = price });
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



