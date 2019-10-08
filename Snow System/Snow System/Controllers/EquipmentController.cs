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
        public ActionResult Index(string searchBy, string search)
        {
                    
                return View(db.Equipments.Where(x => x.Description.StartsWith(search) || search == null).ToList());
    
        }
       
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcEquipmentModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Equipment/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcEquipmentModel>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(mvcEquipmentModel eqp)
        {
            if (eqp.EquipmentID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Equipment", eqp).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Equipment/" + eqp.EquipmentID, eqp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Equipment/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}