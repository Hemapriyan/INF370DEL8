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
        public ActionResult Index(string searchBy, string search)
        {
            
                return View(db.EmployeeTypes.Where(x => x.Description.StartsWith(search) || search == null).ToList());
            
            
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcEmployeeTypeModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("EmployeeType/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcEmployeeTypeModel>().Result);
            }
        }

        [HttpPost]

        public ActionResult AddorEdit(mvcEmployeeTypeModel emp)
        {
            if (emp.EmployeeTypeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("EmployeeType", emp).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("EmployeeType/" + emp.EmployeeTypeID, emp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("EmployeeType/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}