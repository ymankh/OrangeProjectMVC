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
    public class voter_userController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: voter_user
        public ActionResult Index(int? page)
        {
            int itemPerPage = 20;
            if (page == null) page = 0;
            var voter_user = db.voter_user.Include(v => v.district);
            return View(voter_user.OrderBy(u => u.id).Skip((int)(page * itemPerPage)).Take(itemPerPage ).ToList());
        }

        // GET: voter_user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voter_user voter_user = db.voter_user.Find(id);
            if (voter_user == null)
            {
                return HttpNotFound();
            }
            return View(voter_user);
        }

        // GET: voter_user/Create
        public ActionResult Create()
        {
            ViewBag.district_id = new SelectList(db.districts, "id", "name");
            return View();
        }

        // POST: voter_user/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,national_id,name,email,password,district_id,birth_date,gender,religion,ethnicity,has_locally_voted,has_party_voted,first_login")] voter_user voter_user)
        {
            if (ModelState.IsValid)
            {
                db.voter_user.Add(voter_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.district_id = new SelectList(db.districts, "id", "name", voter_user.district_id);
            return View(voter_user);
        }

        // GET: voter_user/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voter_user voter_user = db.voter_user.Find(id);
            if (voter_user == null)
            {
                return HttpNotFound();
            }
            ViewBag.district_id = new SelectList(db.districts, "id", "name", voter_user.district_id);
            return View(voter_user);
        }

        // POST: voter_user/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,national_id,name,email,password,district_id,birth_date,gender,religion,ethnicity,has_locally_voted,has_party_voted,first_login")] voter_user voter_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voter_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.district_id = new SelectList(db.districts, "id", "name", voter_user.district_id);
            return View(voter_user);
        }

        // GET: voter_user/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voter_user voter_user = db.voter_user.Find(id);
            if (voter_user == null)
            {
                return HttpNotFound();
            }
            return View(voter_user);
        }

        // POST: voter_user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            voter_user voter_user = db.voter_user.Find(id);
            db.voter_user.Remove(voter_user);
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
