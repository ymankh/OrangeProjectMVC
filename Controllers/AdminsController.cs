using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OrangeProjectMVC.Models;

namespace OrangeProjectMVC.Controllers
{
    public class AdminsController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: Admins

        private bool NotAdmin()
        {
            if (Session["adminId"] == null)
                return true;
            else
            {
                var id = Convert.ToInt32(Session["adminId"]);
                var xAdmin = db.Admins.FirstOrDefault(user => user.id == id);
                if (xAdmin == null)
                    return true;
            }
            return false;
        }

        public ActionResult Index()
        {
            if (NotAdmin())
            {
                return RedirectToAction("Index", "UserCycle");
            }
            return View(db.Admins.ToList());
        }



        public ActionResult AdminLogin(string email, string password)
        {

            if (!NotAdmin())
                return RedirectToAction("Index", "Home");

            var admin = db.Admins.FirstOrDefault(user => user.email == email && user.password == password);
            if (admin != null)
            {
                Session["adminId"] = admin.id;
                Session["adminName"] = admin.name;
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
            if (NotAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (NotAdmin())
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
            if (NotAdmin())
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
            if (NotAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var admin = db.Admins.Find(id);
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
            if (NotAdmin())
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
            if (NotAdmin())
                return RedirectToAction("Index", "UserCycle");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var admin = db.Admins.Find(id);
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
            if (NotAdmin())
                return RedirectToAction("Index", "UserCycle");
            var admin = db.Admins.Find(id);
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
