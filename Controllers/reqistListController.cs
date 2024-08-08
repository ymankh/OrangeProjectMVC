using OrangeProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace OrangeProjectMVC.Controllers
{

    public class reqistListController : Controller
    {
        // it should be 41, We put 5 for testing only.
        int PartyListSeats = 5 - 1;
        private electionEntities db = new electionEntities();
        // GET: reqistList
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult LocalList()
        {
            //Session["Message"] = "هذه تجربة لميزة الرسائل";
            var distrects = db.districts.ToList();
            return View(distrects);
        }

        public ActionResult SingleLocalList(int? id)
        {

            var distrect = db.districts.Find(id);
            ViewBag.competitive_seat = distrect.competitive_seat;
            ViewBag.women_seats = distrect.women_seats;
            ViewBag.christian_seats = distrect.christian_seats;
            Session["Distinctid"] = id;
            return View(distrect);
        }
        [HttpPost]
        public ActionResult SingleLocalList(int? id, string list_name, string[] competitive_seats, string women_seat, string christian_seat)
        {

            id = Convert.ToInt32(Session["Distinctid"].ToString());

            var distrect = db.districts.Find(id);
            var newList = new election_list_request();
            newList.district_id = id;
            newList.name = list_name;
            newList.status = "pending";
            newList.type = "L";
            db.election_list_request.Add(newList);
            db.SaveChanges();
            var savedLsit = db.election_list_request.FirstOrDefault(x => x.name == list_name);
            foreach (string nationalId in competitive_seats)
            {
                var canedet_user = db.voter_user.FirstOrDefault(user => user.national_id == nationalId);
                if (canedet_user == null)
                {
                    Session["Message"] = "تاكد من ادخال ارقام وطنية صحيحة وحاول مرة اخرى.";
                    return RedirectToAction("SingleLocalList", new { id });
                }
                var canedet = new candidate_request();
                canedet.user_id = canedet_user.id;
                canedet.election_list_request_id = savedLsit.id;
                canedet.type_of_chair = "C";
                db.candidate_request.Add(canedet);
            }
            // For the women seat 
            if (!String.IsNullOrEmpty(women_seat))
            {
                var canedet_user = db.voter_user.FirstOrDefault(user => user.national_id == women_seat);
                if (canedet_user != null && canedet_user.gender == "F")
                {
                    var canedet = new candidate_request();
                    canedet.user_id = canedet_user.id;
                    canedet.election_list_request_id = savedLsit.id;
                    canedet.type_of_chair = "W";
                    db.candidate_request.Add(canedet);

                }
                else
                {
                    Session["Message"] = "يجب ادخال سيدة في مقعد الكوتة";
                    return RedirectToAction("SingleLocalList", new { id = id });
                }
            }
            if (!String.IsNullOrEmpty(christian_seat))
            {
                var canedet_user = db.voter_user.FirstOrDefault(user => user.national_id == christian_seat);
                if (canedet_user != null && canedet_user.religion == "Christian")
                {
                    var canedet = new candidate_request();
                    canedet.user_id = canedet_user.id;
                    canedet.election_list_request_id = savedLsit.id;
                    canedet.type_of_chair = "H";
                    db.candidate_request.Add(canedet);
                }
                else
                {
                    Session["Message"] = "يجب ادخال مسيحي في المقعد المسيحي";
                    return RedirectToAction("SingleLocalList", new { id = id });
                }
            }
            db.SaveChanges();
            Session["SwalMessage"] = "شكرا لقد تم استلام طلبك بنجاح";
            return RedirectToAction("Index", "UserCycle");

        }

        [HttpPost]
        public ActionResult SavePartList(string lsitPartyName)
        {
            var previousSearches = Session["PreviousSearches"] as List<voter_user> ?? new List<voter_user>();
            if (previousSearches.Count == PartyListSeats)
            {
                var newList = new election_list_request();
                newList.name = lsitPartyName;
                newList.district_id = null;
                newList.type = "L";
                newList.status = "Pending";
                db.election_list_request.Add(newList);
                db.SaveChanges();

                int listId = newList.id;
                foreach (var voter in previousSearches)
                {
                    var candidate = new candidate_request();
                    candidate.user_id = voter.id;
                    candidate.election_list_request_id = newList.id;
                    candidate.type_of_chair = "C";
                    db.candidate_request.Add(candidate);
                }
                db.SaveChanges();
                Session["PreviousSearches"] = null;
                Session["SwalMessage"] = "شكرا لقد تم استلام طلبك بنجاح";
            }
            return RedirectToAction("Index", "UserCycle");
        }

        public ActionResult PartyList(string partyListName)
        {
            // Retrieve previously searched voters from the session
            var previousSearches = Session["PreviousSearches"] as List<voter_user> ?? new List<voter_user>();
            ViewBag.partyListName = "partyListName";
            ViewBag.numberOfSeates = PartyListSeats;
            ViewBag.candetCount = previousSearches.Count;
            // Pass the previous searches to the view
            return View(previousSearches);
        }

        [HttpPost]
        public ActionResult PartyList(string partyListName, string nationalID)
        {
            // Fetch the voter based on the national ID
            var voter = db.voter_user.Where(x => x.national_id == nationalID).ToList();

            // Retrieve previously searched voters from the session
            var previousSearches = Session["PreviousSearches"] as List<voter_user> ?? new List<voter_user>();
            ViewBag.numberOfSeates = PartyListSeats;

            ViewBag.candetCount = previousSearches.Count;
            if (previousSearches.Count >= PartyListSeats)
            {
                return View(previousSearches);
            }

            // Add the current search result to the previous searches list
            foreach (var v in voter)
            {
                if (!previousSearches.Any(x => x.national_id == v.national_id))
                {
                    previousSearches.Add(v);
                }
            }

            Session["PreviousSearches"] = previousSearches;

            return View(previousSearches);
        }

    }
}