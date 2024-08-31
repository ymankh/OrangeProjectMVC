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

            ViewBag.irbidFirstVoteCount = FindDistrectVotersCount(1);
            ViewBag.irbidSecondVoteCount = FindDistrectVotersCount(2);
            ViewBag.ajlonVoteCount = FindDistrectVotersCount(3);

            long localVoteCount = db.voter_user.Count(voter => voter.has_locally_voted);
            long partyVoteCount = db.voter_user.Count(voter => voter.has_party_voted);
            long numberOfVoters = db.voter_user.Count();



            ViewBag.distrect_voter_count = localVoteCount;
            ViewBag.party_voter_count = partyVoteCount;
            ViewBag.number_of_lists = db.election_list.Count();
            ViewBag.voting_ratio = Math.Floor((Math.Max(localVoteCount, partyVoteCount) / (double)numberOfVoters) * 100);

            ViewBag.womenVoters = db.voter_user.
                Count(voter => voter.gender == "F" && (voter.has_party_voted || voter.has_locally_voted));
            ViewBag.menVoters = db.voter_user.Count(voter => voter.gender == "M" && (voter.has_party_voted || voter.has_locally_voted));

            return View();
        }

        private long FindDistrectVotersCount(int distrectId)
        {
            long localVotes = db.voter_user.
                Count(voter => voter.district_id == distrectId && voter.has_locally_voted);

            long partyVotes = db.voter_user.
                Count(voter => voter.district_id == distrectId && voter.has_party_voted);
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