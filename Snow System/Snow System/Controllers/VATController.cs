using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace Snow_System.Controllers
{
    public class VATController : Controller
    {
        // GET: VAT
        public ActionResult Index()
        {
            IEnumerable<mvcVATModel> vatList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("VAT").Result;
            vatList = response.Content.ReadAsAsync<IEnumerable<mvcVATModel>>().Result;

            return View(vatList);
            
        }


        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcVATModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("VAT/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcVATModel>().Result);
            }
        }


        [HttpPost]

        public ActionResult AddorEdit(mvcVATModel vatr)
        {
            if (vatr.VATID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("VAT", vatr).Result;
                TempData["SuccessMessage"] = "Saved Successfully";


            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("VAT/" + vatr.VATID, vatr).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("VAT/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";

            return RedirectToAction("Index");

        }
    }
}