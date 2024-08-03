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

        public ActionResult Index()
        {
            long local_vote_count = db.election_list.Where(list => list.type == "L").Sum(list => list.vote_count);
            long party_vote_count = db.election_list.Where(list => list.type == "P").Sum(list => list.vote_count);
            long number_of_voters = db.voter_user.Count();
            ViewBag.distrect_voter_count = local_vote_count;
            ViewBag.party_voter_count = party_vote_count;
            ViewBag.number_of_lists = db.election_list.Count(); 
            @ViewBag.voting_ratio = Math.Floor((Math.Max(local_vote_count, party_vote_count) / (double)number_of_voters) * 100);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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