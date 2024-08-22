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

    public class DistrictWithSeats
    {
        public district district;
        public List<ListsWithSeats> lists;
    }

    public class ListsWithSeats
    {
        public election_list list;
        public double seatsCount;
        public List<candidate> competitiveWinners;
        public candidate womenWinner;
        public candidate christianWinner;
    }

    public class voter_userController : Controller
    {
        private electionEntities db = new electionEntities();

        public ActionResult Results()
        {
            var electionDates = db.Dates.FirstOrDefault();
            if (electionDates.election_end_date > DateTime.Now)
                return RedirectToAction("Index", "UserCycle");

            var resultsDate = db.Dates.FirstOrDefault().results_date;
            // Retrieve all districts from the database
            var districts = db.districts;

            // Retrieve all local election lists and include related districts
            var LocalLists = db.election_list
                .Where(list => list.type == "L")
                .Include(list => list.district);

            // Initialize a list to store the results for each district
            var listOfDistricts = new List<DistrictWithSeats>();

            // Iterate over each district
            foreach (var _district in districts)
            {
                // Count the number of voters who have locally voted in the district
                var locallyVotedCount = db.voter_user
                    .Where(voter => voter.district_id == _district.id)
                    .Count(voter => voter.has_locally_voted);

                // Calculate the vote threshold (7% of locally voted count)
                var threshold = (int)Math.Floor(locallyVotedCount * 0.07);

                // Retrieve local lists for the current district
                var districtLocalLists = LocalLists
                    .Where(list => list.district == _district);

                // Retrieve lists that have votes over the threshold
                var listsOverThreshold = LocalLists
                    .Where(list => list.district_id == _district.id)
                    .Where(list => list.vote_count > threshold)
                    .ToList();

                // Sum the votes of lists over the threshold
                var overThresholdVotesCount = listsOverThreshold.Sum(list => list.vote_count);

                // Initialize a list to store lists with seats
                var overThresholdLists = new List<ListsWithSeats>();

                // Calculate the number of seats for each list over the threshold
                foreach (var list in listsOverThreshold)
                {
                    var seatsPercentage = (double)list.vote_count / overThresholdVotesCount;
                    var numberOfSeats = seatsPercentage * _district.competitive_seat;
                    var listWithSeats = new ListsWithSeats
                    {
                        list = list,
                        seatsCount = numberOfSeats
                    };
                    overThresholdLists.Add(listWithSeats);
                }

                // Calculate the total whole seats assigned
                var holeSeats = overThresholdLists.Sum(list => (int)Math.Truncate(list.seatsCount));

                // Calculate the remaining partial seats to be assigned
                var partialSeats = _district.competitive_seat - holeSeats;

                // Assign partial seats based on the largest decimal part
                if (partialSeats > 0)
                    overThresholdLists = overThresholdLists.OrderByDescending(list =>
                    {
                        var number = list.seatsCount;
                        var integerPart = Math.Floor(number);
                        var decimalPart = number - integerPart;
                        return decimalPart;
                    }).ToList();

                // Distribute remaining partial seats
                for (var i = 0; i < partialSeats; i++)
                {
                    overThresholdLists[i].seatsCount++;
                }

                // Retrieve and assign winners for each list
                var candidates = db.candidates
                    .OrderByDescending(candidate => candidate.vote_count)
                    .Where(x => true);

                var overThresholdListsIdsArray = overThresholdLists.Select(x => x.list.id).ToArray();

                var womenWinner = candidates
                    .Where(candidate => candidate.type_of_chair == "W" && overThresholdListsIdsArray.Contains(candidate.election_list_id))
                    .OrderByDescending(c => c.vote_count)
                    .FirstOrDefault();

                candidate christianWinner = null;

                if (_district.christian_seats > 0)
                {
                    christianWinner = candidates
                        .Where(candidate => candidate.type_of_chair == "H" && overThresholdListsIdsArray.Contains(candidate.election_list_id))
                        .OrderByDescending(x => x.vote_count)
                        .FirstOrDefault();
                }

                foreach (var list in overThresholdLists)
                {
                    // Assign competitive winners to each list
                    list.competitiveWinners = candidates
                        .Where(candidate => candidate.election_list.id == list.list.id)
                        .Take((int)list.seatsCount)
                        .ToList();

                    // Assign women winner if applicable
                    if (womenWinner != null && womenWinner.election_list_id == list.list.id)
                        list.womenWinner = womenWinner;

                    // Assign Christian winner if applicable
                    if (christianWinner != null && christianWinner.election_list_id == list.list.id)
                        list.christianWinner = christianWinner;
                }

                // Create and add the result for the current district
                var districtWithSeats = new DistrictWithSeats
                {
                    district = _district,
                    lists = overThresholdLists
                };
                listOfDistricts.Add(districtWithSeats);
            }

            var partyThreshold = db.voter_user.Count(user => user.has_party_voted) * 0.025;
            var partyListsOverThreshold = db.election_list.
                Include(list => list.candidates).
                Where(list => list.type == "P" && list.vote_count > partyThreshold);
            var partyOverThresholdCount = partyListsOverThreshold.
                Sum(list => list.vote_count);

            var partyListsWithSeats = new List<ListsWithSeats>();
            foreach (var partyList in partyListsOverThreshold)
            {
                var partSeatsCount = ((double)partyList.vote_count / partyOverThresholdCount) * 41;
                var partyListWithSeats = new ListsWithSeats
                {
                    list = partyList,
                    seatsCount = partSeatsCount
                };
                partyListsWithSeats.Add(partyListWithSeats);
            }
            // Calculate the total whole seats assigned
            var partyHoleSeats = partyListsWithSeats.
                Sum(list => (int)Math.
                Truncate(list.seatsCount));

            // Calculate the remaining partial seats to be assigned
            var partyPartialSeats = 41 - partyHoleSeats;

            // Assign partial seats based on the largest decimal part
            if (partyPartialSeats > 0)
                partyListsWithSeats = partyListsWithSeats.OrderByDescending(list =>
                {
                    var number = list.seatsCount;
                    var integerPart = Math.Floor(number);
                    var decimalPart = number - integerPart;
                    return decimalPart;
                }).ToList();

            // Distribute remaining partial seats
            for (var i = 0; i < partyPartialSeats; i++)
            {
                partyListsWithSeats[i].seatsCount++;
            }
            // Find the hole seats for each list
            foreach (var list in partyListsWithSeats)
            {
                // Assign competitive winners to each list
                list.competitiveWinners = list.list.candidates.
                    OrderBy(candidate => candidate.id).
                    Take((int)list.seatsCount).
                    ToList();
            }

            ViewBag.partyListsWithSeats = partyListsWithSeats;
            // Return the view with the results

            var resultShon = DateTime.Now >= resultsDate;
            ViewBag.ResultShon = resultShon;
            @ViewBag.Date = resultsDate;

            return View(listOfDistricts);
        }


        // GET: voter_user
        [Route("admin/voters/{int?}")]
        public ActionResult Index(int? page)
        {
            var filter = new VoterFilterViewModel
            {
                NationalId = Request.QueryString["nationalId"],
                Name = Request.QueryString["name"],
                Email = Request.QueryString["email"],
                BirthDateStart = string.IsNullOrEmpty(Request.QueryString["birthDateStart"]) ? (DateTime?)null : DateTime.Parse(Request.QueryString["birthDateStart"]),
                BirthDateEnd = string.IsNullOrEmpty(Request.QueryString["birthDateEnd"]) ? (DateTime?)null : DateTime.Parse(Request.QueryString["birthDateEnd"]),
                Gender = Request.QueryString["gender"],
                Religion = Request.QueryString["religion"],
                District = Request.QueryString["district"],
                LocallyVoted = Request.QueryString["locallyVoted"] == "on",
                PartyVoted = Request.QueryString["partyVoted"] == "on"
            };
            var voters = db.voter_user.Include(v => v.district); // Assuming you have a Voters DbSet in your DbContext

            if (!string.IsNullOrEmpty(filter.NationalId))
            {
                voters = voters.Where(v => v.national_id.Contains(filter.NationalId));
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                voters = voters.Where(v => v.name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                voters = voters.Where(v => v.email.Contains(filter.Email));
            }

            if (filter.BirthDateStart.HasValue)
            {
                voters = voters.Where(v => v.birth_date >= filter.BirthDateStart.Value);
            }

            if (filter.BirthDateEnd.HasValue)
            {
                voters = voters.Where(v => v.birth_date <= filter.BirthDateEnd.Value);
            }

            if (!string.IsNullOrEmpty(filter.Gender))
            {
                voters = voters.Where(v => v.gender == filter.Gender);
            }

            if (!string.IsNullOrEmpty(filter.Religion))
            {
                voters = voters.Where(v => v.religion == filter.Religion);
            }

            if (!string.IsNullOrEmpty(filter.District))
            {
                voters = voters.Where(v => v.district_id == Convert.ToInt32(filter.District));
            }

            if (filter.LocallyVoted == true)
            {
                voters = voters.Where(v => v.has_locally_voted);
            }

            if (filter.PartyVoted == true)
            {
                voters = voters.Where(v => v.has_party_voted);
            }

            // Change to display more pages in one single page
            var itemPerPage = 20;

            ViewBag.numberOfPages = Math.Ceiling((double)voters.Count() / itemPerPage);
            if (page == null) page = 0;
            return View(voters.OrderByDescending(u => u.id).Skip((int)(page * itemPerPage)).Take(itemPerPage).ToList());
        }


        // GET: voter_user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var voter_user = db.voter_user.Find(id);
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
            var voter_user = db.voter_user.Find(id);
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
            var voter_user = db.voter_user.Find(id);
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
            var voter_user = db.voter_user.Find(id);
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
    public class VoterFilterViewModel
    {
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDateStart { get; set; }
        public DateTime? BirthDateEnd { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string District { get; set; }
        public bool? LocallyVoted { get; set; }
        public bool? PartyVoted { get; set; }

        public string ToQueryString()
        {
            var propertyMappings = new Dictionary<string, string>
        {
            { nameof(NationalId), "nationalId" },
            { nameof(Name), "name" },
            { nameof(Email), "email" },
            { nameof(BirthDateStart), "birthDateStart" },
            { nameof(BirthDateEnd), "birthDateEnd" },
            { nameof(Gender), "gender" },
            { nameof(Religion), "religion" },
            { nameof(District), "district" },
            { nameof(LocallyVoted), "locallyVoted" },
            { nameof(PartyVoted), "partyVoted" }
        };

            var queryString = new List<string>();

            foreach (var property in propertyMappings)
            {
                var propertyInfo = this.GetType().GetProperty(property.Key);
                var value = propertyInfo?.GetValue(this);
                if (value == null)
                {
                    queryString.Add($"{property.Value}=");
                }
                else if (property.Key == nameof(LocallyVoted) && value is bool locallyVotedValue)
                {
                    queryString.Add($"{property.Value}={(locallyVotedValue ? "on" : "off")}");
                }
                else if (property.Key == nameof(PartyVoted) && value is bool partyVotedValue)
                {
                    queryString.Add($"{property.Value}={(partyVotedValue ? "on" : "off")}");
                }
                else if (value is DateTime dateTime)
                {
                    queryString.Add($"{property.Value}={HttpUtility.UrlEncode(dateTime.ToString("yyyy-MM-dd"))}");
                }
                else
                {
                    queryString.Add($"{property.Value}={HttpUtility.UrlEncode(value.ToString())}");
                }
            }
            return "?" + string.Join("&", queryString);
        }

    }
}
