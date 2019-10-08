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
        public ActionResult Index(string searchBy, string search)
        {
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
                return View(new mvcVehicleModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Vehicle/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcVehicleModel>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(mvcVehicleModel veh)
        {
            if (veh.VehicleID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Vehicle", veh).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Vehicle/" + veh.VehicleID, veh).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Vehicle/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }


    }
}