using OrangeProjectMVC.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace OrangeProjectMVC.Controllers
{
    public class ContactUsController : Controller
    {
        private electionEntities db = new electionEntities();

        // GET: ContactUs
        public ActionResult Index()
        {
            var contacts = db.contact_us.ToList();
            return View(contacts);
        }

        public ActionResult contactus()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult contactus(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = new contact_us
                {
                    name = model.Name,
                    email = model.Email,
                    subject = model.Subject,
                    message = model.Message,
                    status = "New"
                };

                db.contact_us.Add(contact);
                db.SaveChanges();

                // Send email notification
                SendEmailNotification(model);

                TempData["SuccessMessage"] = "شكرا لقد تم استلام رسالتك";
                return RedirectToAction("contactus");
            }

            return View(model);
        }

        private void SendEmailNotification(ContactUsViewModel model)
        {
            try
            {
                var fromEmail = "techlearnhub.contact@gmail.com"; // Replace with your email
                var fromPassword = "lyrlogeztsxclank"; // Replace with your email password
                var toEmail = "mohammaddfawareh@gmail.com";

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = "New Contact Us Submission",
                    Body = $"Name: {model.Name}\nEmail: {model.Email}\nSubject: {model.Subject}\nMessage: {model.Message}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exception (log it, show a message to the user, etc.)
                TempData["ErrorMessage"] = $"There was an error sending the email: {ex.Message}";
            }
        }

        public ActionResult Details(int id)
        {
            var contact = db.contact_us.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: ContactUs/Edit/5
        public ActionResult Edit(int id)
        {
            var contact = db.contact_us.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: ContactUs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(contact_us contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: ContactUs/Delete/5
        public ActionResult Delete(int id)
        {
            var contact = db.contact_us.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contact = db.contact_us.Find(id);
            db.contact_us.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}