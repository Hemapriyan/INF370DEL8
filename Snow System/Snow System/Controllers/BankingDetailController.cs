using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class BankingDetailController : Controller
    {
        SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        // GET: BankingDetail

        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();
        public ActionResult AddorEdit()
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Banking Details";
            v.ChangesMade = "Viewed Banking Details";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("BankingDetail/" + "1").Result;
                return View(response.Content.ReadAsAsync<BankingDetail>().Result);
           
        }

        [HttpPost]

        public ActionResult AddorEdit(BankingDetail emp)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("BankingDetail/" + emp.BankingID, emp).Result;
            TempData["SuccessMessage"] = "Updated Successfully";
            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Banking Details";
            n.ChangesMade = "Updated Banking Details";
            n.AuditLogTypeID = 3;
            //n.UserID = eqp.user.UserID;
            db.AuditLogs.Add(n);
            return RedirectToAction("AdminDashboard", "Home");

        }
    }
}