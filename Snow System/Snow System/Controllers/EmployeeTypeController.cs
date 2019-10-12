using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class EmployeeTypeController : Controller
    {
        // GET: EmployeeType
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();
        public ActionResult Index(string searchBy, string search)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee Type";
            v.ChangesMade = "Viewed Employee Type";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            db.SaveChanges();

            return View(db.EmployeeTypes.Where(x => x.Description.StartsWith(search) || search == null).ToList());
            
            
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new EmployeeType());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("EmployeeType/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EmployeeType>().Result);
            }
        }

        [HttpPost]

        public ActionResult AddorEdit(EmployeeType emp)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee Type";
            v.ChangesMade = "Viewed Employee Type";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee Type";
            n.ChangesMade = "Created New Employee Type";
            n.AuditLogTypeID = 2;
            //n.UserID = eqp.user.UserID;
            db.AuditLogs.Add(n);
            db.SaveChanges();
            if (emp.EmployeeTypeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("EmployeeType", emp).Result;
                v.DateAccessed = DateTime.Now;
                v.TableAccessed = "Employee Type";
                v.ChangesMade = "Viewed Employee Type";
                v.AuditLogTypeID = 1;
                //v.UserID = eqp.user.UserID;
                db.AuditLogs.Add(v);

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Employee Type";
                n.ChangesMade = "Created New Employee Type";
                n.AuditLogTypeID = 2;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("EmployeeType/" + emp.EmployeeTypeID, emp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Employee Type";
                n.ChangesMade = "Updated Employee Type";
                n.AuditLogTypeID = 3;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("EmployeeType/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Employee Type";
            v.ChangesMade = "Viewed Employee Type";
            v.AuditLogTypeID = 1;
            //v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Employee Type";
            n.ChangesMade = "Deleted Employee Type";
            n.AuditLogTypeID = 4;
            //n.UserID = id;
            db.AuditLogs.Add(n);

            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}