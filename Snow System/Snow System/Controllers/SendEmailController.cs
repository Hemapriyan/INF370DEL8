using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class SendEmailController : Controller
    {
        // GET: SendEmail
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("u15194711@tuks.co.za", "Spartan Fire");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "AmmaH1987";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                        ViewBag.Status = "Email Sent Successfully.";
                    }
                       
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
                ViewBag.Status = "Email not sent Successfully.";
            }
            return View();
        }

        public void NewSupplierOrderEmail(SupplierOrder so)
        {
            if (ModelState.IsValid)
            {
                var senderEmail = new MailAddress("u15194711@tuks.co.za", "Spartan Fire");
                var receiverEmail = new MailAddress(so.Supplier.EmailAddress , "Receiver");
                var password = "AmmaH1987";
                var sub = "Spartan Fire - New Order - " + DateTime.Now.ToString("yyyy/MM/dd");

                string body = "Dear " + so.Supplier.SupplierName + "<br> Please see the following table of items we would like to order from you <br/>" + 
                    "<table> <tr> " +
                    "<td> Product </td> " +
                    "<td> Quantity </td>" +
                    "</tr>";

                foreach (SupplierOrderLine sol in so.SupplierOrderLines)
                {
                    body+= "<tr> <td> " + sol.Product.Name + "</td>> <td>" + sol.QuantityOrdered +"</td> </tr>";
                }
                body += "</table>" +
                    "<br/><br/>" +
                    "Regards Spartan Fire";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                MailMessage mess = new MailMessage(senderEmail, receiverEmail);
                mess.IsBodyHtml = true;
                mess.Subject = sub;
                mess.Body = body;
                {
                    smtp.Send(mess);
                }
            }
        }
    }
    //test
}