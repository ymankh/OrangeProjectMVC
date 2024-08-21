﻿using OrangeProjectMVC.Models;
using System.IO;
using System;
using System.Linq;
using System.Web;
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
        public ActionResult Create(Ad model, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Generate a unique filename and save the image
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + "_" + Guid.NewGuid().ToString() + extension;

                    // Define the path to save the uploaded file
                    string directoryPath = Server.MapPath("~/uploads/ads/");

                    // Ensure the directory exists
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Save the file
                    string path = Path.Combine(directoryPath, fileName);
                    ImageFile.SaveAs(path);

                    // Save the file path in the database
                    model.img_url = "~/uploads/ads/" + fileName;
                }

                model.status = "pending";
                db.Ads.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Payment");
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



