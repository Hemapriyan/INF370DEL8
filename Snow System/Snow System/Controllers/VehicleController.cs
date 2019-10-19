using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace Snow_System.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        AuditLog n = new AuditLog();
        AuditLog v = new AuditLog();
        public ActionResult Index(string searchBy, string search)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Vehicle";
            v.ChangesMade = "Viewed Vehicles Table";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);
            db.SaveChanges();
            if (searchBy == "Description")
            {
                return View(db.Vehicles.Where(x => x.Description.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Vehicles.Where(x => x.LicenseNumber.StartsWith(search) || search == null).ToList());

            }
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                Vehicle cust = new Vehicle();


                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("VehicleType").Result;

                cust.VehicleTypeList = response.Content.ReadAsAsync<List<VehicleType>>().Result;
                return View(cust);
            }
                //return View(new Vehicle());
            else
            {
                //HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Vehicle/" + id.ToString()).Result;
                //return View(response.Content.ReadAsAsync<Vehicle>().Result);
                Vehicle cust = new Vehicle();
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Vehicle/" + id.ToString()).Result;
                cust = response.Content.ReadAsAsync<Vehicle>().Result;
                cust.VehicleTypeList = db.VehicleTypes.ToList();
                var temp = cust.PurchaseDate.ToString("dd-MM-yyyy");
                cust.PurchaseDate = Convert.ToDateTime(temp).Date;
                return View(cust);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(Vehicle veh, int VehicleTypeID)
        {
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Vehicle";
            v.ChangesMade = "Viewed Vehicle";
            v.AuditLogTypeID = 1;
            //v.UserID = eqp.user.UserID;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Vehicle";
            n.ChangesMade = "Created New Vehicle";
            n.AuditLogTypeID = 2;
            //n.UserID = eqp.user.UserID;
            db.AuditLogs.Add(n);
            db.SaveChanges();

            //CODE FOR DROPDOWNLIST
            Vehicle model_ = new Vehicle();

            model_ = veh;
            veh.VehicleTypeID = VehicleTypeID;
            model_.LicenseNumber = veh.LicenseNumber.ToUpper();
            

            if (veh.VehicleID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Vehicle", model_).Result;
                v.DateAccessed = DateTime.Now;
                v.TableAccessed = "Vehicle";
                v.ChangesMade = "Viewed Vehicle";
                v.AuditLogTypeID = 1;
                //v.UserID = eqp.user.UserID;
                db.AuditLogs.Add(v);

                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Vehicle";
                n.ChangesMade = "Created New Vehicle";
                n.AuditLogTypeID = 2;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Vehicle/" + model_.VehicleID, model_).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
                n.DateAccessed = DateTime.Now;
                n.TableAccessed = "Vehicle";
                n.ChangesMade = "Updated Vehicle";
                n.AuditLogTypeID = 3;
                //n.UserID = eqp.user.UserID;
                db.AuditLogs.Add(n);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Vehicle/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            v.DateAccessed = DateTime.Now;
            v.TableAccessed = "Vehicle";
            v.ChangesMade = "Viewed Vehicle";
            v.AuditLogTypeID = 1;
            //v.UserID = id;
            db.AuditLogs.Add(v);

            n.DateAccessed = DateTime.Now;
            n.TableAccessed = "Vehicle";
            n.ChangesMade = "Deleted Vehicle";
            n.AuditLogTypeID = 4;
            //n.UserID = id;
            db.AuditLogs.Add(n);

            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}