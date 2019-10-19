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
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
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
                    var receiverEmail = new MailAddress("justin.michel.95@gmail.com", "Receiver");
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
                    body+= "<tr> <td> " + sol.Product.Name + "</td> <td>" + sol.QuantityOrdered +"</td> </tr>";
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

        public void OrderConfrimed(ProductOrder po)
        {
            double temp = 0;
            double total = 0;
            db.Configuration.ProxyCreationEnabled = false;
            if (ModelState.IsValid)
            {
                //po.Location.Client.User.UserEmail
                var senderEmail = new MailAddress("u15194711@tuks.co.za", "Spartan Fire");
                var receiverEmail = new MailAddress( "justin.michel.95@gmail.com" , "Receiver");
                var password = "AmmaH1987";
                var sub = "Spartan Fire - New Order - " + DateTime.Now.ToString("yyyy/MM/dd");

                string body = "Dear " + po.Location.Client.ClientName + " " + po.Location.Client.ClientSurname + "<br> <h1>Your Order has been placed! </h1><h3> Order Number: "+ po.ProductOrderID + " </h3> <br/>" +
                    "<h3> Order Details </h3>" +
                    "<table> <tr> " +
                    "<td> Product </td> " +
                    "<td> Quantity </td>" +
                    "<td> Price each </td>" +
                    "<td> Subtotal </td>" +
                    "</tr>";

                foreach (ProductOrderLine sol in po.ProductOrderLines)
                {
                    temp = sol.Product.SellingPrice * sol.QuantityOrdered;
                    total += temp;
                    body += "<tr> <td> " + 
                        sol.Product.Name + 
                        " </td> <td>" +
                        sol.QuantityOrdered +
                        "</td> <td>"+
                        sol.Product.SellingPrice.ToString("C2") +
                        "</td> <td>" +
                        temp.ToString("C2") +
                        " </td> </tr> ";
                }
                VAT vt = db.VATs.OrderByDescending(v=>v.VATID).FirstOrDefault();
                double vatamount = total* ( vt.VATRate / 100);
                body += "<tr><td></td><td></td><td>VAT (" + 
                    vt.VATRate + 
                    "%)</td><td>" + 
                    vatamount.ToString("C2") + 
                    "</td></tr>";

                body += "<tr><td></td><td></td><td>Total</td><td>" +
                    total.ToString("C2") +
                    "</td> </tr></table>" +
                    "<br/><br/>";


                BankingDetail bd = db.BankingDetails.FirstOrDefault();
                body += 
                    "<h3>Payment information</h3>"+ 
                    "<table>" +
                    "<tr> <td> Account number: </td> <td> " + bd.BankAccountNumber +
                    "</tr>" +
                    "<tr> <td> Bank: </td> <td> " + bd.BankName +
                    "</tr>" +
                    "<tr> <td> Branch code: </td> <td> " + bd.BranchCode +
                    "</tr>" +
                    "<tr> <td> Account type: </td> <td> " + bd.AccountType +
                    "</tr>" +
                    "</table>";


                body += "<p style='text-align:center;'>Please make the details of your EFT the order number. <br />Please note that your order will not be schedueled untill the order has been payed for. <br /> It might take up to three more days thereafter for the order to be delivered.</p>";

                body += "<br/> For any queries please phone 081 366 9566 <br/> Regards Spartan Fire";
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
}