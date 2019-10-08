using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Snow_System.Controllers
{
    public class ProductTypeController : Controller
    {
        // GET: ProductType

        private SpartanFireDBEntities1 db = new SpartanFireDBEntities1();
        public ActionResult Index(string searchBy, string search)
        {

            return View(db.ProductTypes.Where(x => x.Description.StartsWith(search)|| search == null).ToList());


        }


        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcProductTypeModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("ProductType/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcProductTypeModel>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(mvcProductTypeModel prod)
        {
            if (prod.ProductTypeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("ProductType", prod).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("ProductType/" + prod.ProductTypeID, prod).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("ProductType/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}