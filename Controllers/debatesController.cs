using OrangeProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OrangeProjectMVC.App_Start
{
    public class debatesController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: debates
        public ActionResult Index()
        {
            var debates = db.debates.Include(d => d.election_list).Include(d => d.election_list1);

            return View(debates.ToList());
        }

        // GET: debates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var debate = db.debates.Find(id);
            if (debate == null)
            {
                return HttpNotFound();
            }
            return View(debate);
        }

        // GET: debates/Create
        public ActionResult Create()
        {
            ViewBag.first_list = new SelectList(db.election_list, "id", "name");
            ViewBag.second_list = new SelectList(db.election_list, "id", "name");
            return View();
        }

        // POST: debates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(debate debate)
        {

            if (ModelState.IsValid)
            {

                debate.requested_date = DateTime.Now;
                debate.details = "  ";
                debate.Zoom_link = " ";
                debate.status = "Pending";
                db.debates.Add(debate);
                db.SaveChanges();
                Session["SwalMessage"] = "شكرا لقد تم استلام طلبك بنجاح";
                return RedirectToAction("Index", "UserCycle");
            }

            ViewBag.first_list = new SelectList(db.election_list, "id", "name", debate.first_list);
            ViewBag.second_list = new SelectList(db.election_list, "id", "name", debate.second_list);
            return View(debate);
        }


        // GET: debates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var debate = db.debates.Find(id);
            if (debate == null)
            {
                return HttpNotFound();
            }

            // Add the states list to ViewBag
            ViewBag.StatusList = new SelectList(new List<SelectListItem>
              {
                  new SelectListItem { Value = "Pending", Text = "Pending" },
                   new SelectListItem { Value = "Reject", Text = "Reject" },
                  new SelectListItem { Value = "Accept", Text = "Accept" }
                }, "Value", "Text", debate.status);
            ViewBag.first_list = new SelectList(db.election_list, "id", "name", debate.first_list);
            ViewBag.second_list = new SelectList(db.election_list, "id", "name", debate.second_list);
            return View(debate);
        }

        // POST: debates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,requested_date,topic,first_list,second_list,status,Zoom_link,details,debate_date")] debate debate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.first_list = new SelectList(db.election_list, "id", "name", debate.first_list);
            ViewBag.second_list = new SelectList(db.election_list, "id", "name", debate.second_list);
            return View(debate);
        }

        // GET: debates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var debate = db.debates.Find(id);
            if (debate == null)
            {
                return HttpNotFound();
            }
            return View(debate);
        }

        // POST: debates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var debate = db.debates.Find(id);
            db.debates.Remove(debate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //show debate in home page
        // GET: debates/ShowDebates
        public ActionResult ShowDebates()
        {
            // Fetch debates with the status 'approved' and include related election lists
            var approvedDebates = db.debates
                .Where(d => d.status == "Accept")
                .Include(d => d.election_list)
                .Include(d => d.election_list1)
                .ToList();

            return View(approvedDebates);
        }

        // POST: debates/ShowDebates
        [HttpPost]
        public ActionResult ShowDebates(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var debate = db.debates.Find(id);


            if (debate == null || debate.status != "Accept")
            {
                return HttpNotFound();
            }

            return View(debate);
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
