using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Index()
        {
            return View();
           
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcUserModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("User/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcUserModel>().Result);
            }
        }
        [HttpPost]

        public ActionResult AddorEdit(mvcUserModel clie)
        {
            if (clie.UserID == 0)
            {
                
                clie.UserRoleID += 1;
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("User", clie).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("User/" + clie.UserID, clie).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index","Login");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("User/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }





        [HttpGet]
        public ActionResult ForgotPassword()
        {
            SMSController c = new SMSController();
           c.Send();
            return View();
        }

        //public int ReturnID(User m)
        //{
        //    int id = m.UserID;

        //    return (id);
        //}

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/User/" + emailFor + "/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("embelete@gmail.com", "Spartan Fire");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Belete2011"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>" + "<br/><br/>" + "If this was not you, please ignore the message"
                    + "<br/><br/>" + "Regards" + "<br/><br/>" + "Spartan Fire";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            bool status = false;

            using (SpartanFireDBEntities1 dc = new SpartanFireDBEntities1())
            {
                var account = dc.Users.Where(a => a.UserEmail == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.UserEmail, resetCode, "ResetPassword");
                    //account.ResetPasswordCode = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }


        public ActionResult ResetPassword(int id = 0)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            //if (string.IsNullOrWhiteSpace(id))
            //{
            //    return HttpNotFound();
            //}

            using (SpartanFireDBEntities1 dc = new SpartanFireDBEntities1())
            {
                //var user = dc.Users.Where(a => a.UserEmail == id).FirstOrDefault();
                //if (user != null)
                //{
                User model = new User();
                model.UserID = id;
                return View(model);
                //}
                //else
                //{
                //    return HttpNotFound();
                //}
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(User model)
        {

            try
            {

                var message = "";
                //if (ModelState.IsValid)
                //{

                SpartanFireDBEntities1 unew = new SpartanFireDBEntities1();
                //int num = model.UserID;
                //where model.password = u .password 

                var cc = unew.Users.Where(a => a.UserEmail == model.UserEmail).FirstOrDefault();
                cc.UserPassword = (model.UserPassword);
                //unew.Users.Find(25).UserPassword = model.UserPassword;
                //user.UserEmail = "";
                //dc.Configuration.ValidateOnSaveEnabled = false;
                unew.SaveChanges();
                message = "New password updated successfully";


                //}
                //else
                //{
                //    message = "Something invalid";
                //}
                ViewBag.Message = message;
                return View(model);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }


    }
}