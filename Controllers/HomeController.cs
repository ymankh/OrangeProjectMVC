using Microsoft.Ajax.Utilities;
using OrangeProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private electionEntities db = new electionEntities();


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

        public ActionResult Index()
        {
            if (notAdmin())
                return RedirectToAction("Index", "UserCycle");
            ViewBag.irbidFirstVoteCount = findDistrectVotersCount(1);
            ViewBag.irbidSecondVoteCount = findDistrectVotersCount(2);
            ViewBag.ajlonVoteCount = findDistrectVotersCount(3);

            long local_vote_count = db.election_list.Where(list => list.type == "L").Sum(list => list.vote_count);
            long party_vote_count = db.election_list.Where(list => list.type == "P").Sum(list => list.vote_count);
            long number_of_voters = db.voter_user.Count();

            ViewBag.distrect_voter_count = local_vote_count;
            ViewBag.party_voter_count = party_vote_count;
            ViewBag.number_of_lists = db.election_list.Count();
            @ViewBag.voting_ratio = Math.Floor((Math.Max(local_vote_count, party_vote_count) / (double)number_of_voters) * 100);

            return View();
        }

        private long findDistrectVotersCount(int distrectId)
        {
            long localVotes = db.voter_user.
                Where(voter => voter.district_id == distrectId && voter.has_locally_voted).
                Count();

            long partyVotes = db.voter_user.
                Where(voter => voter.district_id == 3 && voter.has_party_voted).
                Count();
            return Math.Max(localVotes, partyVotes);
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