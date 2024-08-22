using OrangeProjectMVC.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class candidate_requestController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: candidate_request
        public ActionResult Index()
        {
            var candidateRequest = db.candidate_request.Include(c => c.election_list_request).Include(c => c.voter_user).ToList();
            return View(candidateRequest);
        }

        // GET: candidate_request/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidateRequest = db.candidate_request.Find(id);
            if (candidateRequest == null)
            {
                return HttpNotFound();
            }
            return View(candidateRequest);
        }

        // GET: candidate_request/Create
        public ActionResult Create()
        {
            ViewBag.election_list_request_id = new SelectList(db.election_list_request, "id", "name");
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id");
            return View();
        }

        // POST: candidate_request/Create
        // To protect from overpowering attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,election_list_request_id,type_of_chair,user_id")] candidate_request candidate_request)
        {
            if (ModelState.IsValid)
            {
                db.candidate_request.Add(candidate_request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.election_list_request_id = new SelectList(db.election_list_request, "id", "name", candidate_request.election_list_request_id);
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id", candidate_request.user_id);
            return View(candidate_request);
        }

        // GET: candidate_request/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidateRequest = db.candidate_request.Find(id);
            if (candidateRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.election_list_request_id = new SelectList(db.election_list_request, "id", "name", candidateRequest.election_list_request_id);
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id", candidateRequest.user_id);
            return View(candidateRequest);
        }

        // POST: candidate_request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,election_list_request_id,type_of_chair,user_id")] candidate_request candidate_request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate_request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.election_list_request_id = new SelectList(db.election_list_request, "id", "name", candidate_request.election_list_request_id);
            ViewBag.user_id = new SelectList(db.voter_user, "id", "national_id", candidate_request.user_id);
            return View(candidate_request);
        }

        // GET: candidate_request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var candidate_request = db.candidate_request.Find(id);
            if (candidate_request == null)
            {
                return HttpNotFound();
            }
            return View(candidate_request);
        }

        // POST: candidate_request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var candidate_request = db.candidate_request.Find(id);
            db.candidate_request.Remove(candidate_request);
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
