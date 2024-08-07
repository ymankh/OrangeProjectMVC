﻿using Microsoft.Ajax.Utilities;
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
            //if (NotAdmin())
            //{
            //    return RedirectToAction("Index", "UserCycle");
            //}

            ViewBag.irbidFirstVoteCount = FindDistrectVotersCount(1);
            ViewBag.irbidSecondVoteCount = FindDistrectVotersCount(2);
            ViewBag.ajlonVoteCount = FindDistrectVotersCount(3);

            long localVoteCount = db.election_list.Where(list => list.type == "L").Sum(list => list.vote_count);
            long partyVoteCount = db.election_list.Where(list => list.type == "P").Sum(list => list.vote_count);
            long numberOfVoters = db.voter_user.Count();

            ViewBag.distrect_voter_count = localVoteCount;
            ViewBag.party_voter_count = partyVoteCount;
            ViewBag.number_of_lists = db.election_list.Count();
            @ViewBag.voting_ratio = Math.Floor((Math.Max(localVoteCount, partyVoteCount) / (double)numberOfVoters) * 100);

            return View();
        }

        private long FindDistrectVotersCount(int distrectId)
        {
            long localVotes = db.voter_user.
                Count(voter => voter.district_id == distrectId && voter.has_locally_voted);

            long partyVotes = db.voter_user.
                Count(voter => voter.district_id == 3 && voter.has_party_voted);
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