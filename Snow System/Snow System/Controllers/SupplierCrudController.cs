using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class SupplierCrudController : Controller
    {
        // GET: Supplier
        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "ContactNumber")
            {
                return View(db.Suppliers.Where(x => x.ContactNumber.StartsWith(search) || search == null).ToList());
            }
            else if(searchBy=="Registration")
            {
                return View(db.Suppliers.Where(x => x.RegistrationNumber.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Suppliers.Where(x => x.SupplierName.StartsWith(search) || search == null).ToList());

            }
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcSupplierModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Supplier/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcSupplierModel>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(mvcSupplierModel sup)
        {
            if (sup.SupplierID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Supplier", sup).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Supplier/" + sup.SupplierID, sup).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Supplier/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }


    }
}