using OrangeProjectMVC.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class election_list_requestController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: election_list_request
        public ActionResult Index()
        {
            var election_list_request = db.election_list_request.Include(e => e.district);
            return View(election_list_request.ToList());
        }

        // GET: election_list_request/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            election_list_request election_list_request = db.election_list_request.Find(id);
            if (election_list_request == null)
            {
                return HttpNotFound();
            }
            return View(election_list_request);
        }



        // GET: election_list_request/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            election_list_request election_list_request = db.election_list_request.Find(id);
            if (election_list_request == null)
            {
                return HttpNotFound();

            }
            // Add the states list to ViewBag
            ViewBag.StatusList = new SelectList(new List<SelectListItem>
              {
                  new SelectListItem { Value = "Pending", Text = "Pending" },
                   new SelectListItem { Value = "Reject", Text = "Reject" },
                  new SelectListItem { Value = "Accept", Text = "Accept" }
                }, "Value", "Text", election_list_request.status);
            ViewBag.TypeList = new SelectList(new List<SelectListItem>
              {
                  new SelectListItem { Value = "P", Text = "Party" },
                   new SelectListItem { Value = "L", Text = "Local" }
                }, "Value", "Text", election_list_request.type);
            ViewBag.district_id = new SelectList(db.districts, "id", "name", election_list_request.district_id);
            return View(election_list_request);
        }

        // POST: election_list_request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,district_id,type,status")] election_list_request election_list_request, int id)
        {
            if (ModelState.IsValid)
            {
                var newelection_list_request = election_list_request;
                // Find the election_list_request by id
                var entity = db.election_list_request.Where(x => x.id == id).FirstOrDefault();
                var entityType = entity.type;
                var entitydistincrID = entity.district_id;
                if (entity != null && election_list_request.status == "Accept")
                {
                    // Find related candidate_requests
                    var relatedCandidateRequests = db.candidate_request
                        .Where(cr => cr.election_list_request_id == id).ToList();

                    // Remove related candidate_requests
                    foreach (var candidateRequest in relatedCandidateRequests)
                    {
                        db.candidate_request.Remove(candidateRequest);
                    }

                    // Remove the election_list_request
                    db.election_list_request.Remove(entity);

                    election_list election_List1 = new election_list()
                    {
                        name = newelection_list_request.name,
                        district_id = entitydistincrID,
                        type = entityType,
                        vote_count = 0
                    };
                    db.election_list.Add(election_List1);

                    // Save changes to the database
                    db.SaveChanges();

                }
            }

            // Redirect or return as needed
            return RedirectToAction("Index");
        }


        //    //if (election_list_request.status == "Accept")
        //    //{
        //    //    var newList = new election_list();
        //    //    newList.name = election_list_request.name;
        //    //    newList.type = election_list_request.type;


        //    //    var requestedCandidate = db.candidate_request.
        //    //        Where(c => c.election_list_request_id == id).
        //    //        ToList();

        //    //    if (election_list_request.type == "L")
        //    //    {
        //    //        newList.district_id = election_list_request.district_id;
        //    //    }
        //    //    else
        //    //    {
        //    //        newList.district_id = 1;
        //    //    }
        //    //    db.election_list.Add(newList);
        //    //    db.SaveChanges();
        //    //    foreach (var c in requestedCandidate)
        //    //    {
        //    //        var newC = new candidate();
        //    //        newC.user_id = c.user_id;
        //    //        newC.type_of_chair = c.type_of_chair;
        //    //        newC.election_list_id = newList.id;
        //    //        newC.vote_count = 0;

        //    //        db.candidates.Add(newC);
        //    //        db.candidate_request.Remove(c);
        //    //        db.SaveChanges();
        //    //    }
        //    //    var electionList = db.election_list_request.Find(id);
        //    //    db.election_list_request.Remove(electionList);
        //    //    db.SaveChanges();

        //    //}
        //    //ViewBag.district_id = new SelectList(db.districts, "id", "name", election_list_request.district_id);
        //    //return RedirectToAction("Index");
        //}

        // GET: election_list_request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            election_list_request election_list_request = db.election_list_request.Find(id);
            if (election_list_request == null)
            {
                return HttpNotFound();
            }
            return View(election_list_request);
        }

        // POST: election_list_request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            election_list_request election_list_request = db.election_list_request.Find(id);
            db.election_list_request.Remove(election_list_request);
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
