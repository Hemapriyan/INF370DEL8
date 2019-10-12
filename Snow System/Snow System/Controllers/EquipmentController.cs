using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class EquipmentController : Controller
    {
        // GET: Equipment
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();

        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();
        public ActionResult Index(string searchBy, string search)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Equipment";
            v.ChangesMade = "Viewed Equipments Table";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            db.SaveChanges();

            return View(db.Equipments.Where(x => x.Description.StartsWith(search) || search == null).ToList());
    
        }
       
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new Equipment());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Equipment/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Equipment>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(Equipment eqp)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Equipment";
            v.ChangesMade = "Viewed Equipment";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Equipment";
            n.ChangesMade = "Created New Equipment";
            n.AuditLogTypeID = 2;
            //n.UserID = eqp.user.UserID;
            db.AuditLogs.Add(n);
            db.SaveChanges();

            if (eqp.EquipmentID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Equipment", eqp).Result;

                v.DateAccessed = DateTime.Now;
                v.TableAccessed = "Equipment";
                v.ChangesMade = "Viewed Equipment";
                v.AuditLogTypeID = 1;
                //v.UserID = eqp.user.UserID;
                db.AuditLogs.Add(v);

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Equipment";
                n.ChangesMade = "Created New Equipment";
                n.AuditLogTypeID = 2;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Saved Successfully";



            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Equipment/" + eqp.EquipmentID, eqp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Equipment";
                n.ChangesMade = "Updated Equipment";
                n.AuditLogTypeID = 3;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Equipment/" + id.ToString()).Result;
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Equipment";
            v.ChangesMade = "Viewed Equipment";
            v.AuditLogTypeID = 1;
            //v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Equipment";
            n.ChangesMade = "Deleted Equipment";
            n.AuditLogTypeID = 4;
            //n.UserID = id;
            db.AuditLogs.Add(n);

            db.SaveChanges();
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}