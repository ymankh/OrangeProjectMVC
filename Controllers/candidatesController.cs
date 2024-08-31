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
    public class candidatesController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: candidates
        public ActionResult Index()
        {
            var candidates = db.candidates.Include(c => c.election_list).Include(c => c.voter_user);
            return View(candidates.ToList());
        }

        // GET: candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: candidates/Create
        public ActionResult Create()
        {
            ViewBag.election_list_id = new SelectList(db.election_list, "id", "name");
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id");
            return View();
        }

        // POST: candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,election_list_id,type_of_chair,user_id,vote_count")] candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.election_list_id = new SelectList(db.election_list, "id", "name", candidate.election_list_id);
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id", candidate.user_id);
            return View(candidate);
        }

        // GET: candidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.election_list_id = new SelectList(db.election_list, "id", "name", candidate.election_list_id);
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id", candidate.user_id);
            return View(candidate);
        }

        // POST: candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,election_list_id,type_of_chair,user_id,vote_count")] candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.election_list_id = new SelectList(db.election_list, "id", "name", candidate.election_list_id);
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id", candidate.user_id);
            return View(candidate);
        }

        // GET: candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var candidate = db.candidates.Find(id);
            db.candidates.Remove(candidate);
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
