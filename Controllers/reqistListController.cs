using OrangeProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class reqistListController : Controller
    {
        // It should be 41, but we put 5 for testing only.
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
        public ActionResult SingleLocalList(int? id, string list_name, HttpPostedFileBase list_image, string[] competitive_seats, HttpPostedFileBase[] candidate_images, string women_seat, HttpPostedFileBase women_candidate_image, string christian_seat, HttpPostedFileBase christian_candidate_image)
        {
            id = Convert.ToInt32(Session["Distinctid"].ToString());
            
            // Check if the list name already exists in either election_list or election_list_request
            var existingListInElectionList = db.election_list.FirstOrDefault(x => x.name == list_name);
            var existingListInElectionListRequest = db.election_list_request.FirstOrDefault(x => x.name == list_name);

            if (existingListInElectionList != null || existingListInElectionListRequest != null)
            {
                Session["Message"] = "اسم القائمة موجود بالفعل في النظام. يرجى اختيار اسم آخر.";
                return RedirectToAction("SingleLocalList", new { id = id });
            }
            var distrect = db.districts.Find(id);
            var newList = new election_list_request();
            newList.district_id = id;
            newList.name = list_name;
            newList.status = "pending";
            newList.type = "L";
            
            

            // حفظ صورة القائمة
            if (list_image != null && list_image.ContentLength > 0)
            {
                var listImagePath = Path.Combine(Server.MapPath("~/Images/ElectionLists"), Path.GetFileName(list_image.FileName));
                list_image.SaveAs(listImagePath);
                newList.image_url = "/Images/ElectionLists/" + list_image.FileName;
            }

            db.election_list_request.Add(newList);
            db.SaveChanges();
            var savedLsit = db.election_list_request.FirstOrDefault(x => x.name == list_name);

            for (var i = 0; i < competitive_seats.Length; i++)
            {
                var nationalId = competitive_seats[i];
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
                canedet.is_representative = false;

                // حفظ صورة المرشح
                if (candidate_images[i] != null && candidate_images[i].ContentLength > 0)
                {
                    var candidateImagePath = Path.Combine(Server.MapPath("~/Images/Candidates"), Path.GetFileName(candidate_images[i].FileName));
                    candidate_images[i].SaveAs(candidateImagePath);
                    canedet.img_url = "/Images/Candidates/" + candidate_images[i].FileName;
                }

                db.candidate_request.Add(canedet);
            }

            // حفظ صورة المرشحة لمقعد الكوتة
            if (!String.IsNullOrEmpty(women_seat) && women_candidate_image != null)
            {
                var canedet_user = db.voter_user.FirstOrDefault(user => user.national_id == women_seat);
                if (canedet_user != null && canedet_user.gender == "F")
                {
                    var canedet = new candidate_request();
                    canedet.user_id = canedet_user.id;
                    canedet.election_list_request_id = savedLsit.id;
                    canedet.type_of_chair = "W";

                    var candidateImagePath = Path.Combine(Server.MapPath("~/Images/Candidates"), Path.GetFileName(women_candidate_image.FileName));
                    women_candidate_image.SaveAs(candidateImagePath);
                    canedet.img_url = "/Images/Candidates/" + women_candidate_image.FileName;

                    db.candidate_request.Add(canedet);
                }
                else
                {
                    Session["Message"] = "يجب ادخال سيدة في مقعد الكوتة";
                    return RedirectToAction("SingleLocalList", new { id = id });
                }
            }

            // حفظ صورة المرشح للمقعد المسيحي
            if (!String.IsNullOrEmpty(christian_seat) && christian_candidate_image != null)
            {
                var canedet_user = db.voter_user.FirstOrDefault(user => user.national_id == christian_seat);
                if (canedet_user != null && canedet_user.religion == "Christian")
                {
                    var canedet = new candidate_request();
                    canedet.user_id = canedet_user.id;
                    canedet.election_list_request_id = savedLsit.id;
                    canedet.type_of_chair = "H";

                    var candidateImagePath = Path.Combine(Server.MapPath("~/Images/Candidates"), Path.GetFileName(christian_candidate_image.FileName));
                    christian_candidate_image.SaveAs(candidateImagePath);
                    canedet.img_url = "/Images/Candidates/" + christian_candidate_image.FileName;

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
        public ActionResult SavePartList(string lsitPartyName, HttpPostedFileBase partyImage)
        {
            // Check if the list name already exists in either election_list or election_list_request
            var existingListInElectionList = db.election_list.FirstOrDefault(x => x.name == lsitPartyName);
            var existingListInElectionListRequest = db.election_list_request.FirstOrDefault(x => x.name == lsitPartyName);

            if (existingListInElectionList != null || existingListInElectionListRequest != null)
            {
                Session["Message"] = "اسم القائمة الحزبية موجود بالفعل في النظام. يرجى اختيار اسم آخر.";
                return RedirectToAction("PartyList");
            }
            var previousSearches = Session["PreviousSearches"] as List<voter_user> ?? new List<voter_user>();
            if (previousSearches.Count == PartyListSeats)
            {
                var newList = new election_list_request();
                newList.name = lsitPartyName;
                newList.district_id = null;
                newList.type = "P";
                newList.status = "Pending";
                if (partyImage != null && partyImage.ContentLength > 0)
                {
                    var imagePath = Path.Combine(Server.MapPath("~/Images/PartyLists"), Path.GetFileName(partyImage.FileName));
                    partyImage.SaveAs(imagePath);
                    newList.image_url = "/Images/PartyLists/" + partyImage.FileName;
                }
                db.election_list_request.Add(newList);
                db.SaveChanges();

                var listId = newList.id;
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
