using OrangeProjectMVC.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        electionEntities db = new electionEntities();

        public ActionResult Login(bool isResend = false)
        {
            // If the user is already logged in, redirect them to the home page.
            if (Session["National_ID"] != null)
            {
                return RedirectToAction("Index", "UserCycle");
            }

            if (!isResend) return View();

            // Handle resend verification code scenario
            if (Session["tempNational_ID"] != null)
            {
                var national_id = Session["tempNational_ID"].ToString();
                var user = db.voter_user.FirstOrDefault(u => u.national_id == national_id);

                if (user != null)
                {
                    // Generate and send verification code
                    var random = new Random();
                    var randomCode = random.Next(100000, 1000000);
                    Session["ConfCode"] = randomCode.ToString();

                    // Email settings
                    const string fromEmail = "techlearnhub.contact@gmail.com";
                    const string toEmail = "mohammaddfawareh@gmail.com";
                    const string subjectText = "Your Confirmation Code";
                    var messageText = $"Your confirmation code is {randomCode}";

                    const string smtpServer = "smtp.gmail.com";
                    const int smtpPort = 587;
                    const string smtpUsername = "techlearnhub.contact@gmail.com";
                    const string smtpPassword = "lyrlogeztsxclank";

                    // Send the email
                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(fromEmail);
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subjectText;
                        mailMessage.Body = messageText;
                        mailMessage.IsBodyHtml = false;

                        using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
                        {
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                            smtpClient.EnableSsl = true;

                            smtpClient.Send(mailMessage);

                            return RedirectToAction("VerifyCode");
                        }
                    }
                }
                else
                {
                    // Handle case where user is not found
                    ViewBag.Msg = "User not found.";
                    return View();
                }
            }
            else
            {
                // If tempNational_ID is null, redirect to login or show an error
                ViewBag.Msg = "Your session has expired. Please log in again.";
                return View();
            }

            // Just render the login page if not resending a code
        }

        [HttpPost]
        public ActionResult Login(string national_id)
        {
            var user = db.voter_user.FirstOrDefault(u => u.national_id == national_id);

            if (user == null)
            {
                ViewBag.Msg = "الرقم الوطني غير صحيح";
                return View();
            }

            if (user != null && user.first_login == true)
            {
                // Generate and send verification code
                var random = new Random();
                var randomCode = random.Next(100000, 1000000);
                Session["ConfCode"] = randomCode.ToString();

                // Email settings
                const string fromEmail = "techlearnhub.contact@gmail.com";
                const string toEmail = "mohammaddfawareh@gmail.com";  // For testing
                                                             // string toEmail = user.email;  // For production

                const string subjectText = "Your Confirmation Code";
                var messageText = $"Your confirmation code is {randomCode}";

                const string smtpServer = "smtp.gmail.com";
                const int smtpPort = 587;
                const string smtpUsername = "techlearnhub.contact@gmail.com";
                const string smtpPassword = "lyrlogeztsxclank";

                // Send the email
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(fromEmail);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subjectText;
                    mailMessage.Body = messageText;
                    mailMessage.IsBodyHtml = false;

                    using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true;

                        smtpClient.Send(mailMessage);

                        Session["tempNational_ID"] = user.national_id.ToString();

                        ViewBag.VerfyCode = "تم ارسال رمز التحقق على بريدك الالكتروني";

                        //return RedirectToAction("VerifyCode");
                    }
                }
                return View();
            }
            else if (user.first_login == false)
            {
                return RedirectToAction("LoginWithPassword");
            }
            else
            {
                ModelState.AddModelError("", "National ID not found.");
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["National_ID"] = null;
            return RedirectToAction("Index", "UserCycle");
        }




        public ActionResult VerifyCode()
        {
            return View();
        }

        // POST: User/VerifyCode
        [HttpPost]
        public ActionResult VerifyCode(string verificationCode)
        {
            var sentCode = (string)Session["ConfCode"];
            if (verificationCode == sentCode)
            {
                Session["National_ID"] = Session["tempNational_ID"];
                Session["tempNational_ID"] = null;
                return RedirectToAction("ResetPassword");
            }

            ModelState.AddModelError("", "Invalid verification code.");
            ViewBag.Msg = "رقم التحقق غير صحيح";
            return View();
        }



        // GET: User/ResetPassword
        public ActionResult ResetPassword()
        {
            return View();
        }

        // POST: User/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            if (newPassword == confirmPassword)
            {

                var nationalId = Session["National_ID"];
                var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);
                if (user != null)
                {
                    user.password = newPassword;
                    user.first_login = false;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("LoginWithPassword");
                }
            }

            ModelState.AddModelError("", "Passwords do not match.");

            ViewBag.Msg = "كلمات المرور غير متطابقة";
            return View();
        }


        public ActionResult LoginWithPassword()
        {
            if (Session["National_ID"] == null)
                return View();
            Session["National_ID"] = null;
            return RedirectToAction("LoginWithPassword", "User");

        }

        [HttpPost]
        public ActionResult LoginWithPassword(voter_user newUser)
        {

            var user = db.voter_user.FirstOrDefault(u => u.national_id == newUser.national_id && u.password == newUser.password);


            if (user == null)
            {
                ViewBag.Msg = "كلمة المرور او الرقم الوطني غير صحيح";

                return View();

            }
            Session["National_ID"] = newUser.national_id;
            switch (user.district_id)
            {
                case 3:
                    Session["c"] = "ajloun.jpeg";
                    return RedirectToAction("Circles");
                case 1:
                    Session["c"] = "irbid01.jpg";
                    return RedirectToAction("Circles");
                default:
                    Session["c"] = "irbid02.jpg";
                    return RedirectToAction("Circles");
            }

        }

        public ActionResult Circles()
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Index", "UserCycle");
            }
            var nationalId = (string)Session["National_ID"];
            var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);
            if (user.first_login) return RedirectToAction("ResetPassword");
            var userDistrict = user.district_id;

            var circles = db.districts.Find(userDistrict);

            ViewBag.Circle = circles.name;
            return View();
        }

        public ActionResult ListsType()
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var nationalId = (string)Session["National_ID"];
            var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);
            if (user == null) return RedirectToAction("Index", "Home");

            // To Make sure that he has rested the password after the first login
            if (user.first_login) return RedirectToAction("ResetPassword");

            if (user.has_locally_voted)
            {
                ViewBag.display1 = "none";
                ViewBag.voted1 = "لقد قمت بالتصويت محليا";
            }
            if (user.has_party_voted)
            {
                ViewBag.display2 = "none";
                ViewBag.voted2 = "لقد قمت بالتصويت حزبيا";
            }
            ViewBag.LocallyVoted = user.has_locally_voted;
            ViewBag.PartyVoted = user.has_party_voted;
            return View();

        }

        public ActionResult ListsNames()
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var nationalId = (string)Session["National_ID"];
            var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);

            // Ensure the user has reset their password after the first login
            if (user.first_login) return RedirectToAction("ResetPassword");

            var userDistrict = user.district_id;
            var electionList = db.election_list
                .Where(list => list.district_id == userDistrict)
                .Include(list => list.candidates)  // Assuming 'candidates' is the navigation property
                .ToList();

            // Image paths array
            string[] imgs =
            {
                "~/img/Archaeological-Sites-In-Jordan-1.jpg",
                "~/img/ec36e1e53da7b48f126d9d49a0e43681.jpg",
                "~/img/2017_09_14_12_56_41.jpg",
                "~/img/jarash1.jpg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
                "~/img/undraw_profile.svg",
            };

            ViewBag.Images = imgs;

            return View(electionList);
        }



        [HttpPost]
        public ActionResult SingleList(int? election_list_id)
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var nationalId = (string)Session["National_ID"];
            var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);
            if (user == null) return RedirectToAction("Index", "Home");

            // To Make sure that he has rested the password after the first login
            if (user.first_login) return RedirectToAction("ResetPassword");

            if (election_list_id == null)
            {
                user.has_locally_voted = true;
                db.voter_user.AddOrUpdate(user);
                db.SaveChanges();
                ViewBag.LocallyVoted = user.has_party_voted;
                return RedirectToAction("ListsType");
            }

            ViewBag.lsitName = db.election_list.Find(election_list_id).name;
            var electionList = db.election_list.Find(election_list_id).candidates.ToList();
            Session["election_list_id"] = election_list_id;
            return View(electionList); // Pass the electionList to the view
        }

        [HttpPost]
        public ActionResult SaveVote(string[] candidates)
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var nationalId = (string)Session["National_ID"];
            var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);
            if (user == null) return RedirectToAction("Index", "Home");

            // To Make sure that he has rested the password after the first login
            if (user.first_login) return RedirectToAction("ResetPassword");

            user.has_locally_voted = true;
            db.voter_user.AddOrUpdate(user);
            if (candidates != null)
                foreach (var candidate in candidates)
                {
                    var c = db.candidates.Find(Convert.ToInt32(candidate));
                    c.vote_count = c.vote_count + 1;
                    db.candidates.AddOrUpdate(c);
                }
            var election_list_id = (int)Session["election_list_id"];
            var election_list = db.election_list.Find(election_list_id);
            election_list.vote_count += 1;
            db.election_list.AddOrUpdate(election_list);
            db.SaveChanges();
            return RedirectToAction("ListsType");
        }


        public ActionResult PartyListsNames()
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var nationalId = (string)Session["National_ID"];
            var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);
            if (user == null) return RedirectToAction("Index", "Home");

            // To Make sure that he has rested the password after the first login
            if (user.first_login) return RedirectToAction("ResetPassword");

            var userDistrict = user.district_id;
            var electionList = db.election_list
                .Where(list => list.district_id == null)
                .Include(list => list.candidates)  // Assuming 'candidates' is the navigation property
                .ToList();



            return View(electionList);

        }

        public ActionResult PartySaveVote(int? election_list_id)
        {
            if (Session["National_ID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var nationalId = (string)Session["National_ID"];
            var user = db.voter_user.FirstOrDefault(u => u.national_id == nationalId);
            if (user == null) return RedirectToAction("Index", "Home");


            // To Make sure that he has rested the password after the first login
            if (user.first_login) return RedirectToAction("ResetPassword");

            user.has_party_voted = true;
            db.voter_user.AddOrUpdate(user);

            ViewBag.PartyVoted = user.has_party_voted;

            var c = db.election_list.FirstOrDefault(list => list.id == election_list_id);
            if (c != null)
            {
                c.vote_count++;
                db.Entry(c).State = EntityState.Modified;
            }
            db.SaveChanges();


            return RedirectToAction("ListsType");
        }
    }
}
