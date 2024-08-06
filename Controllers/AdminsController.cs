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
    public class AdminsController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: Admins
        public ActionResult Index()
        {

            return View(db.Admins.ToList());
        }
        bool notAdmin()
        {
            if (Session["adminId"] == null)
                return true;
            else
            {
                int id = Convert.ToInt32(Session["adminId"]);
                var xAdmin = db.Admins.FirstOrDefault(user => user.id == id);
                if (xAdmin == null)
                    return true;
            }
            return false;
        }

        public ActionResult AdminLogin(string email, string password)
        {
            if (!notAdmin())
                return RedirectToAction("Index", "Home");

            var admin = db.Admins.FirstOrDefault(user => user.email == email && user.password == password);
            if (admin != null)
            {
                Session["adminId"] = admin.id;
                return RedirectToAction("Index", "Home");
            }
            return View(db.Admins.ToList());
        }

        public ActionResult Logout(string email, string password)
        {
            Session["adminId"] = null;
            return RedirectToAction("Index", "UserCycle");
        }
        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,password")] Admin admin)
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,password")] Admin admin)
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
