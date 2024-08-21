using OrangeProjectMVC.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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
        public ActionResult Edit(election_list_request election_list_request, HttpPostedFileBase image_file)
        {
            if (ModelState.IsValid)
            {
                if (image_file != null && image_file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image_file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/ElectionLists/"), fileName);
                    image_file.SaveAs(path);
                    election_list_request.image_url = "~/Images/ElectionLists/" + fileName;
                }

                var entity = db.election_list_request.Find(election_list_request.id);
                if (entity != null)
                {
                    entity.name = election_list_request.name;
                    entity.type = election_list_request.type;
                    entity.status = election_list_request.status;
                    entity.district_id = election_list_request.district_id;
                    entity.image_url = election_list_request.image_url;

                    if (entity.status == "Accept")
                    {
                        var relatedCandidateRequests = db.candidate_request
                            .Where(cr => cr.election_list_request_id == election_list_request.id).ToList();
                        var election_List1 = new election_list()
                        {
                            name = election_list_request.name,
                            district_id = entity.district_id,
                            type = entity.type,
                            image_url = entity.image_url,
                            vote_count = 0
                        };

                        foreach (var candidateRequest in relatedCandidateRequests)
                        {
                            var newCandidate = new candidate
                            {
                                election_list_id = election_List1.id,
                                img_url = candidateRequest.img_url,
                                is_representative = candidateRequest.is_representative,
                                type_of_chair = candidateRequest.type_of_chair,
                                vote_count = 0,
                                user_id = candidateRequest.user_id,
                            };
                            db.candidates.Add(newCandidate);
                            db.candidate_request.Remove(candidateRequest);
                        }

                        db.election_list_request.Remove(entity);


                        db.election_list.Add(election_List1);
                    }

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            // Reload ViewBag data in case of validation errors
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
            foreach (var candidateRequest in db.candidate_request.Where(request => request.election_list_request_id == id).ToArray())
            {
                db.candidate_request.Remove(candidateRequest);
            }
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
