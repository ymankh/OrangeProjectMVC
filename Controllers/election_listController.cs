using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrangeProjectMVC.Models;

namespace OrangeProjectMVC.Controllers
{
    public class election_listController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: election_list
        public ActionResult Index()
        {
            var election_list = db.election_list.Include(e => e.district);
            return View(election_list.ToList());
        }

        // GET: election_list/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var election_list = db.election_list.Find(id);
            if (election_list == null)
            {
                return HttpNotFound();
            }
            return View(election_list);
        }

        // GET: election_list/Create
        public ActionResult Creae()
        {
            ViewBag.district_id = new SelectList(db.districts, "id", "name");
            return View();
        }

        // POST: election_list/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,district_id,type,vote_count")] election_list election_list)
        {
            if (ModelState.IsValid)
            {
                db.election_list.Add(election_list);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.district_id = new SelectList(db.districts, "id", "name", election_list.district_id);
            return View(election_list);
        }

        // GET: election_list/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var election_list = db.election_list.Find(id);
            if (election_list == null)
            {
                return HttpNotFound();
            }
            ViewBag.district_id = new SelectList(db.districts, "id", "name", election_list.district_id);
            return View(election_list);
        }

        // POST: election_list/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,district_id,type,vote_count")] election_list election_list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(election_list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.district_id = new SelectList(db.districts, "id", "name", election_list.district_id);
            return View(election_list);
        }

        // GET: election_list/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var election_list = db.election_list.Find(id);
            if (election_list == null)
            {
                return HttpNotFound();
            }
            return View(election_list);
        }

        // POST: election_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var election_list = db.election_list.Find(id);
            db.election_list.Remove(election_list);
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
